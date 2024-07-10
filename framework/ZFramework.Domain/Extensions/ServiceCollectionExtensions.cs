using ZFramework.Domain;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class for definning extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all necessary dependencies for the solution's domain layer, such as <see cref="IDomainService"/> instances.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="assemblies">Assemblies of the solution's domain projects used to register some componentes such as instances of <see cref="IDomainService"/> by convention.</param>
        /// <param name="lifetime">Lifetime that the services will have once registered.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services
                .RegisterPublicClassesAsSelf<IDomainService>(assemblies, lifetime);
        }
    }
}