using System.Reflection;

namespace Microsoft.Extensions.Configuration
{
    public static class IConfigurationExtensions
    {
        public static Assembly GetApplicationAssembly(this IConfiguration configuration)
        {
            return configuration.GetAssembly("ApplicationAssembly");
        }

        public static Assembly GetDomainAssembly(this IConfiguration configuration)
        {
            return configuration.GetAssembly("DomainAssembly");
        }

        public static Assembly GetInfrastructureAssembly(this IConfiguration configuration)
        {
            return configuration.GetAssembly("InfrastructureAssembly");
        }

        private static Assembly GetAssembly(this IConfiguration configuration, string assemblyKey)
        {
            var assemblyValue = configuration.GetSection("Assemblies").GetValue<string>(assemblyKey);

            //var assemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().FirstOrDefault(a => a.Name == assemblyValue);

            return Assembly.Load(assemblyValue);
        }
    }
}