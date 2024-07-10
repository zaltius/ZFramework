using AutoMapper;
using ZSample.Domain.Entities.Direcciones;

namespace ZSample.Application.Services.Direcciones.Dtos
{
    public sealed class DireccionMapProfile : Profile
    {
        public DireccionMapProfile()
        {
            CreateMap<Direccion, DireccionReadingDto>()
                .ForMember(src => src.TipoDireccionNombre, opt => opt.MapFrom(dst => dst.TipoDireccion));

            CreateMap<DireccionCreationDto, Direccion>();

            CreateMap<DireccionUpdateDto, Direccion>();
        }
    }
}