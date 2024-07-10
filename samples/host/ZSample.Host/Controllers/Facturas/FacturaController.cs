using ZFramework.Web.Controllers;
using ZSample.Application.Services.Facturas;
using ZSample.Application.Services.Facturas.Dtos;

namespace ZSample.Host
{
    public class FacturaController : ApiController<Guid, IFacturaService, FacturaCreationDto, FacturaUpdateDto, FacturaReadingDto>
    {
        public FacturaController(
            IFacturaService service,
            ILogger<FacturaController> logger)
            : base(service, logger)
        {
        }
    }
}