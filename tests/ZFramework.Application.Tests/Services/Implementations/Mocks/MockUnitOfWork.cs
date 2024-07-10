using ZFramework.Domain.UnitOfWork;

namespace ZFramework.Application.Tests.Services.Implementations.Mocks;

internal sealed class MockUnitOfWork : IUnitOfWork
{
    public bool Enabled { get; set; }

    public bool TransactionActive { get; set; }

    public void Commit()
    {
    }

    public async Task CommitAsync()
    {
        await Task.Run(() => { });
    }

    public ValueTask DisposeAsync()
    {
        return new ValueTask();
    }

    public void Rollback()
    {
    }
}