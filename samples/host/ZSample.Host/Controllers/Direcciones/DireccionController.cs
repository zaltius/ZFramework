using ZFramework.Web.Controllers;
using ZSample.Application.Services.Direcciones;
using ZSample.Application.Services.Direcciones.Dtos;

namespace ZSample.Host.Controllers.Direcciones
{
    public class DireccionController : ApiController<Guid, IDireccionService, DireccionCreationDto, DireccionUpdateDto, DireccionReadingDto>
    {
        public DireccionController(
            IDireccionService service,
            ILogger<DireccionController> logger)
            : base(service, logger)
        {
        }
    }
}