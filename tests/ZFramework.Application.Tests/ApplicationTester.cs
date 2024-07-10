using Microsoft.Extensions.DependencyInjection;
using ZFramework.Application.Tests.Services.Implementations;
using ZFramework.Application.Tests.Services.Implementations.Mocks;
using ZFramework.Domain.Tests.Entities.Implementations;
using ZFramework.Domain.UnitOfWork;
using ZTesting;
using System.Reflection;

namespace ZFramework.Application.Tests;

public abstract class ApplicationTester : TesterBase
{
    protected ITestEntityApplicationService TestEntityApplicationService;

    public ApplicationTester() : base()
    {
        RegisterDependencies();

        ResolveDependencies();
    }

    protected override void RegisterDependencies()
    {
        ServiceCollection
            .AddLogging()
            .AddTransient<IUnitOfWork, MockUnitOfWork>()
            .AddTransient<ITestEntityRepository, MockTestEntityRepository>()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddTransient<ITestEntityApplicationService, TestEntityApplicationService>();

        ServiceProvider = ServiceCollection.BuildServiceProvider();
    }

    protected override void ResolveDependencies()
    {
        TestEntityApplicationService = ServiceProvider!.GetRequiredService<ITestEntityApplicationService>();
    }
}