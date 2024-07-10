using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Scans the given assembly and registers all public non-abstract non-generic classes assignable to <typeparamref name="T"/> as <typeparamref name="T"/> with the given <see cref="ServiceLifetime"/>.
        /// </summary>
        /// <typeparam name="T">Represents the Type that must be assignable from the classes to register.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the classes to.</param>
        /// <param name="assemblies">Assemblies to scan for classes.</param>
        /// <param name="lifetime">Lifetime that the classes will have once registered.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection RegisterPublicClassesAsMatchingInterfaces<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime)
        {
            return services
                .Scan(scan => scan
                    .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo<T>(), publicOnly: true)
                    .AsMatchingInterface()
                    .WithLifetime(lifetime));
        }

        /// <summary>
        /// Scans the given assembly and registers all public non-abstract non-generic classes assignable to <typeparamref name="T"/> as self with the given <see cref="ServiceLifetime"/>.
        /// </summary>
        /// <typeparam name="T">Represents the Type that must be assignable from the classes to register.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the classes to.</param>
        /// <param name="assemblies">Assemblies to scan for classes.</param>
        /// <param name="lifetime">Lifetime that the classes will have once registered.</param>
        /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
        public static IServiceCollection RegisterPublicClassesAsSelf<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime)
        {
            return services
                .Scan(scan => scan
                    .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo<T>(), publicOnly: true)
                    .AsSelf()
                    .WithLifetime(lifetime));
        }
    }
}