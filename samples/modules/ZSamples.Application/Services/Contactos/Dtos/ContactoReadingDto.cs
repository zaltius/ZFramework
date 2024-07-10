using ZFramework.Application.Services.Dtos.Auditing;

namespace ZSample.Application.Services.Contactos.Dtos
{
    public class ContactoReadingDto : AuditedEntityDto<Guid>
    {
        public Guid ClienteId { get; set; }

        public string ClienteRazonSocial { get; set; }

        public string Direccion { get; set; }

        public bool InitialCreation { get; set; }

        public string Nombre { get; set; }
    }
}