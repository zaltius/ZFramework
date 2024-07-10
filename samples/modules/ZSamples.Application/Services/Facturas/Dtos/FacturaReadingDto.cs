using ZFramework.Application.Services.Dtos.Auditing;

namespace ZSample.Application.Services.Facturas.Dtos
{
    public class FacturaReadingDto : AuditedEntityDto<Guid>
    {
        public decimal BaseImponible { get; set; }

        public Guid ClienteId { get; protected set; }

        public string ClienteRazonSocial { get; internal set; }

        public DateTime FechaEntrada { get; set; }

        public decimal ImporteIva { get; set; }

        public string Numero { get; set; }

        public decimal PctIva { get; set; }

        public string Serie { get; set; }

        public decimal Total { get; set; }
    }
}