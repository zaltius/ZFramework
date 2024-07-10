using ZFramework.Application.Services.Dtos.Auditing;

namespace ZSample.Application.Services.Clientes.Dtos
{
    public class ClienteUpdateDto : ConcurrencyAwareEntityDto<Guid>
    {
        public string Cif { get; set; }

        public DateTime? FechaConstitucion { get; set; }

        public string RazonSocial { get; set; }
    }
}