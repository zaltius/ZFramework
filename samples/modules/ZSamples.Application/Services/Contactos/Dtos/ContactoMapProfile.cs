using AutoMapper;
using ZSample.Domain.Entities.Contactos;

namespace ZSample.Application.Services.Contactos.Dtos
{
    public sealed class ContactoMapProfile : Profile
    {
        public ContactoMapProfile()
        {
            CreateMap<Contacto, ContactoReadingDto>()
                .ForMember(src => src.ClienteRazonSocial, opt => opt.MapFrom(dst => dst.Cliente.RazonSocial));

            CreateMap<ContactoCreationDto, Contacto>();

            CreateMap<ContactoUpdateDto, Contacto>();
        }
    }
}