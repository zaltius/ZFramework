using Microsoft.Extensions.DependencyInjection;

namespace ZTesting;

/// <summary>
/// Base class for test-oriented classes.
/// Provides some base methods and properties to help build a tester class with Microsoft's dependency injection.
/// </summary>
public abstract class TesterBase
{
    protected readonly IServiceCollection ServiceCollection;

    protected IServiceProvider? ServiceProvider;

    public TesterBase()
    {
        ServiceCollection = new ServiceCollection();
    }

    protected virtual void RegisterDependencies()
    {
    }

    protected virtual void ResolveDependencies()
    {
    }

    protected virtual void SetUp()
    {
    }
}