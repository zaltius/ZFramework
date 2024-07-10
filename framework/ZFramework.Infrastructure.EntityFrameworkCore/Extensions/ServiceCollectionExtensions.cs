using Microsoft.EntityFrameworkCore;
using ZFramework.Domain.EntityFrameworkCore;
using ZFramework.Domain.UnitOfWork;
using ZFramework.Infrastructure.EntityFrameworkCore;
using ZFramework.Infrastructure.EntityFrameworkCore.UnitOfWork;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class for definning extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers public, non-abstract <see cref="IRepository"/> instances in the provided <see cref="Assembly"/> as their matching interface.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="lifetime">Lifetime that the classes will have once registered. <see cref="ServiceLifetime.Transient"/> by default.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddRepositoriesByConvention(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services
                .RegisterPublicClassesAsMatchingInterfaces<IRepository>(assemblies, lifetime);
        }

        /// <summary>
        /// Registers the specified context and a unit of work to use transactional-like operations within it.
        /// </summary>
        /// <typeparam name="TDbContext">Represents an instance of <see cref="EfCoreDbContext"/>, used as the main DbContext.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="dbContextOptions">The options to build the context from.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddTransactionalDbContext<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
            where TDbContext : EfCoreDbContext
        {
            services.AddUnitOfWork<TDbContext>();

            return services.AddDbContext<TDbContext>(dbContextOptions);
        }

        /// <summary>
        /// Registers an instance of <see cref="EfCoreUnitOfWork{TDbContext}"/> to use interact with the provided context.
        /// </summary>
        /// <typeparam name="TDbContext">Represents an instance of <see cref="EfCoreDbContext"/> to interact with.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services) where TDbContext : EfCoreDbContext
        {
            return services.AddTransient<IUnitOfWork, EfCoreUnitOfWork<TDbContext>>();
        }
    }
}