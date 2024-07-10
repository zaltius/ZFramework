using ZFramework.Web.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Adds JWT authentication to the application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configuration"> Configuration file containing JWT-related sections.</param>
        /// <param name="jwtBearerEvents">Developer-defined events to customize the authentication process.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, JwtBearerEvents? jwtBearerEvents = null)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions));

            var secretKey = jwtOptions[nameof(JwtOptions.SecretKey)];

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            services.Configure<JwtOptions>(options =>
                options.SecretKey = jwtOptions[nameof(JwtOptions.SecretKey)]);

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = signingKey,
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;

                    options.Events = jwtBearerEvents ?? new JwtAuthenticationEvents();
                });

            return services;
        }

        /// <summary>
        /// Adds a default configuration to use JWT with Swagger.
        /// Method <see cref="SwaggerGenServiceCollectionExtensions.AddSwaggerGen(IServiceCollection, Action{Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions})"/> should be called first.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSwaggerJwtAuthentication(this IServiceCollection services)
        {
            services.ConfigureSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **only**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });
            });

            return services;
        }
    }
}
