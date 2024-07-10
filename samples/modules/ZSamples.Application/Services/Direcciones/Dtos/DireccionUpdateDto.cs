using ZFramework.Application.Services.Dtos.Auditing;
using ZSample.Domain.Entities.TiposDirecciones;

namespace ZSample.Application.Services.Direcciones.Dtos
{
    public class DireccionUpdateDto : ConcurrencyAwareEntityDto<Guid>
    {
        public string Calle { get; set; }

        public string CodigoPostal { get; set; }

        public string Descripcion { get; set; }

        public string Poblacion { get; set; }

        public TipoDireccionEnum TipoDireccion { get; set; }
    }
}