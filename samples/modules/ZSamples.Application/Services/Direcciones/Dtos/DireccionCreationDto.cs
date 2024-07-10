using ZFramework.Application.Services.Dtos;
using ZSample.Domain.Entities.TiposDirecciones;

namespace ZSample.Application.Services.Direcciones.Dtos
{
    public class DireccionCreationDto : EntityDto
    {
        public string Calle { get; set; }

        public virtual Guid ClienteId { get; set; }

        public string CodigoPostal { get; set; }

        public string Descripcion { get; set; }

        public string Poblacion { get; set; }

        public TipoDireccionEnum TipoDireccion { get; set; }
    }
}