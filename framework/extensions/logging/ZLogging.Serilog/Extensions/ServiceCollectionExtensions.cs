using Microsoft.Extensions.Configuration;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class for definning extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string DefaultConfigFileName = "appsettings.json";

        /// <summary>
        /// Adds Serilog as logging provider configurable by configuration file.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add Serilog support to.</param>
        /// <param name="configFileName">Configuration file name where Serilog configuration is stored.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSerilog(this IServiceCollection services, string configFileName = DefaultConfigFileName)
        {
            return services
                .AddLogging(loggingBuilder =>
                    loggingBuilder.AddSerilog(InitializeLogger(configFileName), dispose: true));
        }

        private static ILogger InitializeLogger(string configFileName)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configFileName)
                .Build();

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return loggerConfiguration;
        }
    }
}