using ZFramework.Application.Services.Dtos.Auditing;
using ZSample.Domain.Entities.TiposDirecciones;

namespace ZSample.Application.Services.Direcciones.Dtos
{
    public class DireccionReadingDto : AuditedEntityDto<Guid>
    {
        public string Calle { get; set; }

        public string CodigoPostal { get; set; }

        public string Descripcion { get; set; }

        public string Poblacion { get; set; }

        public string Provincia { get; set; }

        public TipoDireccionEnum TipoDireccion { get; set; }

        public string TipoDireccionNombre { get; set; }
    }
}