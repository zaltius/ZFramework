using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using ZFramework.Domain.Tests.Entities.Implementations;
using ZFramework.Domain.UnitOfWork;
using ZFramework.Infrastructure.EntityFrameworkCore.Tests.Implementations;
using ZInfrastructure.EntityFrameworkCore.Sqlite.Extensions;
using ZTesting;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Tests;

public abstract class InfrastructureTester : TesterBase
{
    protected TestDbContext TestDbContext;

    protected ITestEntityRepository TestEntityRepository;

    protected IUnitOfWork UnitOfWork;

    public InfrastructureTester() : base()
    {
        RegisterDependencies();

        ResolveDependencies();
    }

    protected override void RegisterDependencies()
    {
        var sqliteConnection = new SqliteConnection("Data Source=:memory:");

        sqliteConnection.Open();

        ServiceCollection
            .AddTransient<ITestEntityRepository, TestEntityRepository>()
            .AddTransactionalDbContext<TestDbContext>(o => o.UseSqlitePersistence(sqliteConnection));

        ServiceProvider = ServiceCollection.BuildServiceProvider();

        ServiceProvider.GetRequiredService<TestDbContext>().Database.EnsureCreated();
    }

    protected override void ResolveDependencies()
    {
        TestEntityRepository = ServiceProvider!.GetRequiredService<ITestEntityRepository>();

        UnitOfWork = ServiceProvider!.GetRequiredService<IUnitOfWork>();

        TestDbContext = ServiceProvider!.GetRequiredService<TestDbContext>();
    }
}