using ZFramework.Infrastructure.EntityFrameworkCore.Repositories;
using ZSample.Domain.Entities.Facturas;


namespace ZSample.Infrastructure.Repositories.Facturas
{
    public class FacturaRepository : RepositoryBase<Factura, Guid, ApplicationDbContext>, IFacturaRepository
    {
        public FacturaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}