using Microsoft.AspNetCore.Builder;
using ZFramework.Web.Middlewares;

namespace ZFramework.Web.Extensions
{
    /// <summary>
    /// Class for definning extension methods for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds a CORS middleware to your web application pipeline to allow any cross domain requests.
        /// </summary>
        /// <param name="applicationBuilder">Represents the class that provides the mechanisms to configure the application's request pipeline.</param>
        /// <returns>The same <see cref="IApplicationBuilder"/> so that multiple calls can be chained.</returns>
        public static IApplicationBuilder UseCorsAllowAny(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        }

        /// <summary>
        /// Adds a middleware to your web application pipeline to serve standarized responses for every request.
        /// </summary>
        /// <param name="applicationBuilder">Represents the class that provides the mechanisms to configure the application's request pipeline.</param>
        /// <returns>The same <see cref="IApplicationBuilder"/> so that multiple calls can be chained.</returns>
        public static IApplicationBuilder UseStandardHttpResponses(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<StandardHttpResponseMiddleware>();
        }
    }
}