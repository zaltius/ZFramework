using ZFramework.Application.Services.Dtos;

namespace ZSample.Application.Services.Facturas.Dtos
{
    public class FacturaCreationDto : EntityDto
    {
        public decimal BaseImponible { get; set; }

        public Guid ClienteId { get; set; }

        public string Numero { get; set; }

        public decimal PctIva { get; set; }

        public string Serie { get; set; }
    }
}