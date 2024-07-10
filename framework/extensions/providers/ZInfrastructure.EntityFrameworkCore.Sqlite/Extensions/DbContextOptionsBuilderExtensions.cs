using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ZInfrastructure.EntityFrameworkCore.Sqlite.Extensions
{
    /// <summary>
    /// Class for definning extension methods for <see cref="DbContextOptionsBuilder"/>.
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures the options to use SQLite persistence and optional use of lazy-loading.
        /// </summary>
        /// <param name="options">The options to configure.</param>
        /// <param name="connection">Represents a connection to a SQLite database.</param>
        /// <param name="sqliteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseSqlitePersistence(this DbContextOptionsBuilder options, SqliteConnection connection, Action<SqliteDbContextOptionsBuilder>? sqliteOptionsAction = null, bool useLazyLoading = true)
        {
            return options
                .UseSqlite(connection, sqliteOptionsAction)
                .UseLazyLoadingProxies(useLazyLoading);
        }

        /// <summary>
        /// Configures the options to use SQLite persistence and optional use of lazy-loading.
        /// </summary>
        /// <param name="options">The options to configure.</param>
        /// <param name="connectionString">Connection string to the database represented by the context.</param>
        /// <param name="sqliteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseSqlitePersistence(this DbContextOptionsBuilder options, string connectionString, Action<SqliteDbContextOptionsBuilder>? sqliteOptionsAction = null, bool useLazyLoading = true)
        {
            return options
                .UseSqlite(connectionString, sqliteOptionsAction)
                .UseLazyLoadingProxies(useLazyLoading);
        }
    }
}