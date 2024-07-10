using ZFramework.Application.Services.Dtos;

namespace ZSample.Application.Services.Clientes.Dtos
{
    public class ClienteCreationDto : EntityDto
    {
        public string Cif { get; set; }

        public DateTime? FechaConstitucion { get; set; }

        public string RazonSocial { get; set; }
    }
}