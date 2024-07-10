using AutoMapper;
using ZSample.Domain.Entities.ClientesDirecciones;


namespace ZSample.Application.Services.ClientesDirecciones.Dtos
{
    public sealed class ClienteDireccionMapProfile : Profile
    {
        public ClienteDireccionMapProfile()
        {
            CreateMap<ClienteDireccion, ClienteDireccionReadingDto>();

            CreateMap<ClienteDireccionCreationDto, ClienteDireccion>();

            CreateMap<ClienteDireccionUpdateDto, ClienteDireccion>();
        }
    }
}