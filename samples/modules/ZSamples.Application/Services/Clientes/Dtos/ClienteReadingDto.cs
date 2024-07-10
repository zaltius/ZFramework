using ZFramework.Application.Services.Dtos.Auditing;
using ZSample.Application.Services.Contactos.Dtos;
using ZSample.Application.Services.Direcciones.Dtos;

namespace ZSample.Application.Services.Clientes.Dtos
{
    public class ClienteReadingDto : AuditedEntityDto<Guid>
    {
        public string Cif { get; set; }

        public IList<ContactoReadingDto> Contactos { get; internal set; }

        public DireccionReadingDto DireccionFiscal { get; internal set; }

        public DateTime? FechaConstitucion { get; set; }

        public decimal ImporteFacturasCurrentYear { get; set; }

        public string RazonSocial { get; set; }
    }
}