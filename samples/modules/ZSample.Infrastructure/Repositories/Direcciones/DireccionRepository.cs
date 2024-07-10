using ZFramework.Infrastructure.EntityFrameworkCore.Repositories;
using ZSample.Domain.Entities.Direcciones;

namespace ZSample.Infrastructure.Repositories.Direcciones
{
    public class DireccionRepository : RepositoryBase<Direccion, Guid, ApplicationDbContext>, IDireccionRepository
    {
        public DireccionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}