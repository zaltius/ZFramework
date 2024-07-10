using ZFramework.Infrastructure.EntityFrameworkCore.Repositories;
using ZSample.Domain.Entities.Contactos;

namespace ZSample.Infrastructure.Repositories.Contactos
{
    public class ContactoRepository : RepositoryBase<Contacto, Guid, ApplicationDbContext>, IContactoRepository
    {
        public ContactoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Contacto? GetInitialByClienteId(Guid clienteId)
        {
            return DbSet.FirstOrDefault(c => c.ClienteId == clienteId && c.InitialCreation == true);
        }
    }
}