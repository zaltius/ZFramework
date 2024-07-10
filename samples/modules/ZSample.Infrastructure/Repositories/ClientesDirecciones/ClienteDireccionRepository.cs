using ZFramework.Infrastructure.EntityFrameworkCore.Repositories;
using ZSample.Domain.Entities.ClientesDirecciones;


namespace ZSample.Infrastructure.Repositories.ClientesDirecciones
{
    public class ClienteDireccionRepository : RepositoryBase<ClienteDireccion, Guid, ApplicationDbContext>, IClienteDireccionRepository
    {
        public ClienteDireccionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}