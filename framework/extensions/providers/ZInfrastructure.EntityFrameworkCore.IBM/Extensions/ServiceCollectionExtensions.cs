using IBM.EntityFrameworkCore;
using IBM.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZFramework.Infrastructure.EntityFrameworkCore;

namespace ZInfrastructure.EntityFrameworkCore.IBM
{
    namespace Microsoft.Extensions.DependencyInjection
    {
        /// <summary>
        /// Class for definning extension methods for <see cref="IServiceCollection"/>.
        /// </summary>
        public static class ServiceCollectionExtensions
        {
            /// <summary>
            /// Adds a context with Db2 persistence.
            /// </summary>
            /// <typeparam name="TDbContext">Represents the type of the context to configure.</typeparam>
            /// <param name="services">The <see cref="IServiceCollection"/> to add the DbContext to.</param>
            /// <param name="connectionString">Connection string to the database represented by the context.</param>
            /// <param name="db2OptionsAction">An optional action to allow additional Db2 specific configuration.</param>
            /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
            /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
            public static IServiceCollection AddDb2DbContext<TDbContext>(this IServiceCollection services, string connectionString, Action<Db2DbContextOptionsBuilder>? db2OptionsAction = null, bool useLazyLoading = true)
                where TDbContext : EfCoreDbContext
            {
                return services
                    .AddDbContext<TDbContext>(options => options
                        .UseDb2(connectionString, db2OptionsAction)
                        .UseLazyLoadingProxies(useLazyLoading));
            }
        }
    }
}