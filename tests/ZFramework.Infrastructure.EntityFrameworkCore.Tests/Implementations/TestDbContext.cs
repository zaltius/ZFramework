using Microsoft.EntityFrameworkCore;
using ZFramework.Domain.Tests.Entities.Implementations;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Tests.Implementations;

public class TestDbContext : EfCoreDbContext
{
    public DbSet<TestEntity> EntityTests => Set<TestEntity>();

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }
}