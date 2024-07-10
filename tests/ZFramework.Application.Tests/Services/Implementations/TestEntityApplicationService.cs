using AutoMapper;
using Microsoft.Extensions.Logging;
using ZFramework.Application.Services;
using ZFramework.Domain.EntityFrameworkCore;
using ZFramework.Domain.Tests.Entities.Implementations;
using ZFramework.Domain.UnitOfWork;

namespace ZFramework.Application.Tests.Services.Implementations;

internal sealed class TestEntityApplicationService
    : ApplicationService<TestEntity, Guid, ITestEntityRepository, TestEntityDto, TestEntityDto, TestEntityDto>, ITestEntityApplicationService
{
    public TestEntityApplicationService(
        ILogger<TestEntityApplicationService> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ITestEntityRepository repository)
        : base(logger, mapper, unitOfWork, repository)
    {
    }
}