using ZFramework.Domain.EntityFrameworkCore;

namespace ZSample.Domain.Entities.Facturas
{
    public interface IFacturaRepository : IRepository<Factura, Guid>
    {
    }
}