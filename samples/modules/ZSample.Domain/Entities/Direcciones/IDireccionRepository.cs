using ZFramework.Domain.EntityFrameworkCore;

namespace ZSample.Domain.Entities.Direcciones
{
    public interface IDireccionRepository : IRepository<Direccion, Guid>
    {
    }
}