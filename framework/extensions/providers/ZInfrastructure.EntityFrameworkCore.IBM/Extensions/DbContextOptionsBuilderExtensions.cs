using IBM.EntityFrameworkCore;
using IBM.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Class for definning extension methods for <see cref="DbContextOptionsBuilder"/>.
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures the options to use Db2 persistence and optional use of lazy-loading.
        /// </summary>
        /// <param name="options">The options to configure.</param>
        /// <param name="connectionString">Connection string to the database represented by the context.</param>
        /// <param name="db2OptionsAction">An optional action to allow additional Db2 specific configuration.</param>
        /// <param name="useLazyLoading">An optional flag to enable/disable the use of lazy loading. Enabled by default.</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseDb2Persistence(this DbContextOptionsBuilder options, string connectionString, Action<Db2DbContextOptionsBuilder>? db2OptionsAction = null, bool useLazyLoading = true)
        {
            return options
                .UseDb2(connectionString, db2OptionsAction)
                .UseLazyLoadingProxies(useLazyLoading);
        }
    }
}