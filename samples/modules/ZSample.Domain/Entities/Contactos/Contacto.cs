using ZFramework.Domain.Entities.Auditing;
using ZSample.Domain.Entities.Clientes;

namespace ZSample.Domain.Entities.Contactos
{
    public class Contacto : FullAuditedEntity<Guid>
    {
        public virtual Cliente Cliente { get; protected set; } = null!;

        // Name matching pattern: https://aka.ms/efcore-relationships
        public virtual Guid ClienteId { get; set; }

        public string? Direccion { get; set; }

        public bool InitialCreation { get; set; } = false;

        public string Nombre { get; set; } = null!;
        // Creación no restringida mediante constructor.
    }
}