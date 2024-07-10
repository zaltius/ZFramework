using ZFramework.Application.Services.Dtos;

namespace ZSample.Application.Services.Contactos.Dtos
{
    public class ContactoCreationDto : EntityDto
    {
        public Guid ClienteId { get; set; }

        public string Direccion { get; set; }

        public string Nombre { get; set; }
    }
}