using ZFramework.Web.Controllers;
using ZSample.Application.Services.Contactos;
using ZSample.Application.Services.Contactos.Dtos;

namespace ZSample.Host.Controllers.Contactos
{
    public class ContactoController : ApiController<Guid, IContactoService, ContactoCreationDto, ContactoUpdateDto, ContactoReadingDto>
    {
        public ContactoController(
            IContactoService service,
            ILogger<ContactoController> logger)
            : base(service, logger)
        {
        }
    }
}