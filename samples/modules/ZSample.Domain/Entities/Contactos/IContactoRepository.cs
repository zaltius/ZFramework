using ZFramework.Domain.EntityFrameworkCore;

namespace ZSample.Domain.Entities.Contactos
{
    public interface IContactoRepository : IRepository<Contacto, Guid>
    {
        Contacto? GetInitialByClienteId(Guid clienteId);
    }
}