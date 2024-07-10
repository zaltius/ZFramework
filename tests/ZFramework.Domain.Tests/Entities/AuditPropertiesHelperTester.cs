using FluentAssertions;
using ZFramework.Domain.Entities.Auditing;
using ZFramework.Domain.Tests.Entities.Implementations;
using Xunit;

namespace ZFramework.Domain.Tests.Entities;

public class AuditPropertiesHelperTester : DomainTester
{
    private readonly TestEntity entityTest = new();

    public AuditPropertiesHelperTester()
    {
    }

    [Fact]
    public void Should_correct_deletion_audit_properties_when_deleted()
    {
        var deletionTime = DateTime.UtcNow;
        var deletionUsername = "DELETE";

        entityTest.IsDeleted = true;

        AuditPropertiesHelper.CorrectAddedDeletionAuditProperties(entityTest, deletionTime, deletionUsername);

        entityTest.DeletionTime.Should().Be(deletionTime);
        entityTest.DeletionUsername.Should().Be(deletionUsername);
    }

    [Fact]
    public void Should_correct_deletion_audit_properties_when_not_deleted()
    {
        var deletionTime = DateTime.UtcNow;
        var deletionUsername = "DELETE";

        AuditPropertiesHelper.CorrectAddedDeletionAuditProperties(entityTest, deletionTime, deletionUsername);

        entityTest.DeletionTime.Should().BeNull();
        entityTest.DeletionUsername.Should().BeNull();
    }

    [Fact]
    public void Should_set_creation_audit_properties()
    {
        var creationDate = DateTime.UtcNow;
        var creationUsername = "CREATE";

        AuditPropertiesHelper.SetCreationAuditProperties(entityTest, creationDate, creationUsername);

        entityTest.CreationTime.Should().Be(creationDate);
        entityTest.CreationUsername.Should().Be(creationUsername);
    }

    [Fact]
    public void Should_set_modification_audit_properties()
    {
        var modificationTime = DateTime.UtcNow;
        var modificationUsername = "EDIT";

        AuditPropertiesHelper.SetModificationAuditProperties(entityTest, modificationTime, modificationUsername);

        entityTest.LastModificationTime.Should().Be(modificationTime);
        entityTest.LastModificationUsername.Should().Be(modificationUsername);
    }
}