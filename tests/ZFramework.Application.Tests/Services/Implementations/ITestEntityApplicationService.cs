using ZFramework.Application.Services;

namespace ZFramework.Application.Tests.Services.Implementations;

public interface ITestEntityApplicationService : IApplicationService<Guid, TestEntityDto, TestEntityDto, TestEntityDto>
{
}