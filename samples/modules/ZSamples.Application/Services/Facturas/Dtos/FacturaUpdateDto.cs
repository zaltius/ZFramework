using ZFramework.Application.Services.Dtos.Auditing;

namespace ZSample.Application.Services.Facturas.Dtos
{
    public class FacturaUpdateDto : ConcurrencyAwareEntityDto<Guid>
    {
        public decimal BaseImponible { get; set; }

        public decimal PctIva { get; set; }
    }
}