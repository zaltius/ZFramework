using ZFramework.Application.Services.Dtos;

namespace ZFramework.Application.Tests.Services.Implementations;

public class TestEntityDto : EntityDto<Guid>
{
    public string Property { get; set; }
}