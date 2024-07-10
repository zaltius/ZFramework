using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using ZFramework.Infrastructure.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class for definning extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a context with PostgreSQL persistence.
        /// </summary>
        /// <typeparam name="TDbContext">Represents the type of the context to configure.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the DbContext to.</param>
        /// <param name="connectionString">Connection string to the database represented by the context.</param>
        /// <param name="postgreSqlOptionsAction">An optional action to allow additional PostgreSQL specific configuration.</param>
        /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddPostgreSqlDbContext<TDbContext>(this IServiceCollection services, string connectionString, Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null, bool useLazyLoading = true)
            where TDbContext : EfCoreDbContext
        {
            return services
                .AddDbContext<TDbContext>(options => options
                    .UsePostgreSqlPersistence(connectionString, postgreSqlOptionsAction, useLazyLoading));
        }
    }
}