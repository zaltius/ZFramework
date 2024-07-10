using ZTesting;

namespace ZFramework.Domain.Tests;

public abstract class DomainTester : TesterBase
{
    public DomainTester() : base()
    {
        SetUp();

        RegisterDependencies();

        ResolveDependencies();
    }

    protected override void RegisterDependencies()
    {
    }

    protected override void ResolveDependencies()
    {
    }

    protected override void SetUp()
    {
    }
}