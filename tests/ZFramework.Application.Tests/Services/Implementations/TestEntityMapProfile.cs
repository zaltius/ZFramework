using AutoMapper;
using ZFramework.Domain.Tests.Entities.Implementations;

namespace ZFramework.Application.Tests.Services.Implementations;

public sealed class TestEntityMapProfile : Profile
{
    public TestEntityMapProfile()
    {
        CreateMap<TestEntity, TestEntityDto>();

        CreateMap<TestEntityDto, TestEntity>();
    }
}