using ZFramework.Application.Services.Dtos.Auditing;

namespace ZSample.Application.Services.Contactos.Dtos
{
    public class ContactoUpdateDto : ConcurrencyAwareEntityDto<Guid>
    {
        public string Nombre { get; set; }

        public string Direccion { get; set; }
    }
}