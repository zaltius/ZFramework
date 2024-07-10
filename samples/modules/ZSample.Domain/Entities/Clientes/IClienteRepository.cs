using ZFramework.Common.Filtering;
using ZFramework.Domain.EntityFrameworkCore;

namespace ZSample.Domain.Entities.Clientes
{
    public interface IClienteRepository : IRepository<Cliente, Guid>
    {
        IPagedEnumerable<Cliente> GetHavingFacturasAndDirecciones(IFilteringOptions? filteringOptions = null);
    }
}