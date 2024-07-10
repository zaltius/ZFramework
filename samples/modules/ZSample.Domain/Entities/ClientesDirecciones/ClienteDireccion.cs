using ZFramework.Domain.Entities;
using ZSample.Domain.Entities.Clientes;
using ZSample.Domain.Entities.Direcciones;

namespace ZSample.Domain.Entities.ClientesDirecciones
{
    public class ClienteDireccion : Entity<Guid>
    {
        public virtual Cliente Cliente { get; protected set; } = null!;

        public Guid? ClienteId { get; set; }

        public virtual Direccion Direccion { get; protected set; } = null!;

        public Guid DireccionId { get; set; }
    }
}