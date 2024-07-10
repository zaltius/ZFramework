using ZFramework.Common.Filtering;
using ZFramework.Domain.Tests.Entities.Implementations;

namespace ZFramework.Application.Tests.Services.Implementations.Mocks;

internal sealed class MockTestEntityRepository : ITestEntityRepository
{
    private readonly IList<TestEntity> _entities = new List<TestEntity>();

    public int Count()
    {
        return _entities.Count;
    }

    public void Delete(TestEntity entity)
    {
        _entities.Remove(entity);
    }

    public void DeleteById(Guid id)
    {
        var direccion = _entities.FirstOrDefault(d => d.Id == id);

        if (direccion != null)
        {
            _entities.Remove(direccion);
        }
    }

    public IPagedEnumerable<TestEntity> GetAll(IFilteringOptions? filteringOptions = null)
    {
        return _entities.AsQueryable().PageResult(filteringOptions);
    }

    public IEnumerable<TestEntity> GetByCodigoPostal(string codigoPostal)
    {
        throw new System.NotImplementedException();
    }

    public TestEntity? GetById(Guid id)
    {
        return _entities.FirstOrDefault(d => d.Id == id);
    }

    public TestEntity Insert(TestEntity entity)
    {
        _entities.Add(entity);

        return entity;
    }

    public TestEntity Update(TestEntity entity)
    {
        var testEntity = _entities.FirstOrDefault(d => d.Id == entity.Id);

        _entities.Remove(testEntity);

        if (!testEntity.IsDeleted)
        {
            _entities.Add(entity);
        }

        return entity;
    }
}