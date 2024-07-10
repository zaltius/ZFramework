using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ZFramework.Infrastructure.EntityFrameworkCore;
using ZInfrastructure.EntityFrameworkCore.Sqlite.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class for definning extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a context with SQLite persistence.
        /// </summary>
        /// <typeparam name="TDbContext">Represents the type of the context to configure.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the DbContext to.</param>
        /// <param name="connection">Represents a connection to a SQLite database.</param>
        /// <param name="sqliteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSqliteDbContext<TDbContext>(this IServiceCollection services, SqliteConnection connection, Action<SqliteDbContextOptionsBuilder>? sqliteOptionsAction = null, bool useLazyLoading = true)
            where TDbContext : DbContext
        {
            return services
                .AddDbContext<TDbContext>(options => options
                    .UseSqlitePersistence(connection, sqliteOptionsAction, useLazyLoading));
        }

        /// <summary>
        /// Adds a context with SQLite persistence.
        /// </summary>
        /// <typeparam name="TDbContext">Represents the type of the context to configure.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the DbContext to.</param>
        /// <param name="connectionString">Connection string to the database represented by the context.</param>
        /// <param name="sqliteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSqliteDbContext<TDbContext>(this IServiceCollection services, string connectionString, Action<SqliteDbContextOptionsBuilder>? sqliteOptionsAction = null, bool useLazyLoading = true)
            where TDbContext : EfCoreDbContext
        {
            return services
                .AddDbContext<TDbContext>(options => options
                    .UseSqlitePersistence(connectionString, sqliteOptionsAction, useLazyLoading));
        }
    }
}