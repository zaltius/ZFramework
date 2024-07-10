using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerWithEnvironmentName(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection
                .AddSwaggerGen(c =>
                {
                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);

                    c.DocumentFilter<SwaggerAddEnumDescriptions>();
                    
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "ZSample",
                        Version = "v1",
                        Description = configuration.GetValue<string>("Environment")
                    });
                });
        }

        internal class SwaggerAddEnumDescriptions : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                // add enum descriptions to result models
                foreach (var property in swaggerDoc.Components.Schemas.Where(x => x.Value?.Enum?.Count > 0))
                {
                    IList<IOpenApiAny> propertyEnums = property.Value.Enum;
                    if (propertyEnums != null && propertyEnums.Count > 0)
                    {
                        property.Value.Description += DescribeEnum(propertyEnums, property.Key);
                    }
                }

                // add enum descriptions to input parameters
                foreach (var pathItem in swaggerDoc.Paths)
                {
                    DescribeEnumParameters(pathItem.Value.Operations, swaggerDoc, context.ApiDescriptions, pathItem.Key);
                }
            }

            private string DescribeEnum(IList<IOpenApiAny> enums, string proprtyTypeName)
            {
                List<string> enumDescriptions = new List<string>();
                var enumType = GetEnumTypeByName(proprtyTypeName);
                if (enumType == null)
                    return null;

                foreach (OpenApiInteger enumOption in enums)
                {
                    int enumInt = enumOption.Value;
                    var optionName = Enum.GetName(enumType, enumInt);
                    var optionDescription = (Enum.Parse(enumType, optionName) as Enum);

                    enumDescriptions.Add(string.Format("{0} = {1}", enumInt, optionDescription));
                }

                return string.Join(", ", enumDescriptions.ToArray());
            }

            private void DescribeEnumParameters(IDictionary<OperationType, OpenApiOperation> operations, OpenApiDocument swaggerDoc, IEnumerable<ApiDescription> apiDescriptions, string path)
            {
                path = path.Trim('/');
                if (operations != null)
                {
                    var pathDescriptions = apiDescriptions.Where(a => a.RelativePath == path);
                    foreach (var oper in operations)
                    {
                        var operationDescription = pathDescriptions.FirstOrDefault(a => a.HttpMethod.Equals(oper.Key.ToString(), StringComparison.InvariantCultureIgnoreCase));
                        foreach (var param in oper.Value.Parameters)
                        {
                            var parameterDescription = operationDescription.ParameterDescriptions.FirstOrDefault(a => a.Name == param.Name);
                            if (parameterDescription != null && TryGetEnumType(parameterDescription.Type, out Type enumType))
                            {
                                var paramEnum = swaggerDoc.Components.Schemas.FirstOrDefault(x => x.Key == enumType.Name);
                                if (paramEnum.Value != null)
                                {
                                    param.Description += DescribeEnum(paramEnum.Value.Enum, paramEnum.Key);
                                }
                            }
                        }
                    }
                }
            }

            private Type GetEnumTypeByName(string enumTypeName)
            {
                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .FirstOrDefault(x => x.Name == enumTypeName);
            }

            private Type GetTypeIEnumerableType(Type type)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var underlyingType = type.GetGenericArguments()[0];
                    if (underlyingType.IsEnum)
                    {
                        return underlyingType;
                    }
                }

                return null;
            }

            private bool TryGetEnumType(Type type, out Type enumType)
            {
                if (type == null)
                {
                    enumType = null;
                    return false;
                }

                if (type.IsEnum)
                {
                    enumType = type;
                    return true;
                }
                else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var underlyingType = Nullable.GetUnderlyingType(type);
                    if (underlyingType != null && underlyingType.IsEnum == true)
                    {
                        enumType = underlyingType;
                        return true;
                    }
                }
                else
                {
                    Type underlyingType = GetTypeIEnumerableType(type);
                    if (underlyingType != null && underlyingType.IsEnum)
                    {
                        enumType = underlyingType;
                        return true;
                    }
                    else
                    {
                        var interfaces = type.GetInterfaces();
                        foreach (var interfaceType in interfaces)
                        {
                            underlyingType = GetTypeIEnumerableType(interfaceType);
                            if (underlyingType != null && underlyingType.IsEnum)
                            {
                                enumType = underlyingType;
                                return true;
                            }
                        }
                    }
                }

                enumType = null;
                return false;
            }
        }
    }
}
