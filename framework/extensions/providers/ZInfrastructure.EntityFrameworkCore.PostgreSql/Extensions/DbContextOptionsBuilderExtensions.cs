using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Class for definning extension methods for <see cref="DbContextOptionsBuilder"/>.
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures the options to use PostgreSQL persistence and optional use of lazy-loading.
        /// </summary>
        /// <param name="options">The options to configure.</param>
        /// <param name="connectionString">Connection string to the database represented by the context.</param>
        /// <param name="postgreSqlOptionsAction">An optional action to allow additional PostgreSQL specific configuration.</param>
        /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UsePostgreSqlPersistence(this DbContextOptionsBuilder options, string connectionString, Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null, bool useLazyLoading = true)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return options
                .UseNpgsql(connectionString, postgreSqlOptionsAction)
                .UseLazyLoadingProxies(useLazyLoading);
        }
    }
}