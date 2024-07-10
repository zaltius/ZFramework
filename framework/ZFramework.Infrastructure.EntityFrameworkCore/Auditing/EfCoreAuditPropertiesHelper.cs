using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ZFramework.Common.Authentication;
using ZFramework.Domain.Entities.Auditing;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Auditing
{
    internal static class EfCoreAuditPropertiesHelper
    {
        internal static void ApplyChangesForAuditProperties(List<EntityEntry> entries, ICurrentUser? currentUser)
        {
            foreach (var entry in entries)
            {
                var currentTime = DateTime.UtcNow;
                var currentUsername = currentUser?.Id;

                switch (entry.State)
                {
                    case EntityState.Added:
                        AuditPropertiesHelper.SetCreationAuditProperties(entry.Entity, currentTime, currentUsername ?? string.Empty);
                        AuditPropertiesHelper.SetModificationAuditProperties(entry.Entity, currentTime, currentUsername);
                        AuditPropertiesHelper.CorrectAddedDeletionAuditProperties(entry.Entity, currentTime, currentUsername);

                        break;

                    case EntityState.Modified:
                        AuditPropertiesHelper.SetModificationAuditProperties(entry.Entity, currentTime, currentUsername);
                        CorrectCreationAuditProperties(entry);
                        CorrectModifiedDeletionAuditProperties(entry, currentTime);

                        break;
                }
            }
        }

        private static void CorrectCreationAuditProperties(EntityEntry entry)
        {
            if (entry.Entity is ICreationAudited auditedEntity &&
                entry.OriginalValues[nameof(auditedEntity.CreationTime)] is DateTime originalCreationTime)
            {
                auditedEntity.CreationTime = originalCreationTime;
            }
        }

        private static void CorrectModifiedDeletionAuditProperties(EntityEntry entry, DateTime deletionTime)
        {
            if (entry.Entity is IDeletionAudited auditedEntity)
            {
                var currentIsDeleted = entry.CurrentValues[nameof(auditedEntity.IsDeleted)];
                var originalIsDeleted = entry.OriginalValues[nameof(auditedEntity.IsDeleted)];

                if (currentIsDeleted is bool)
                {
                    var isDeleted = Convert.ToBoolean(currentIsDeleted);

                    if (!currentIsDeleted.Equals(originalIsDeleted) && isDeleted)
                    {
                        auditedEntity.DeletionTime = deletionTime;
                    }
                    else if (currentIsDeleted.Equals(originalIsDeleted) && isDeleted &&
                        entry.OriginalValues[nameof(auditedEntity.DeletionTime)] is DateTime originalDeletionTime)
                    {
                        auditedEntity.DeletionTime = originalDeletionTime;
                    }
                    else if (!currentIsDeleted.Equals(originalIsDeleted) && !isDeleted ||
                        currentIsDeleted.Equals(originalIsDeleted) && !isDeleted)
                    {
                        auditedEntity.DeletionTime = null;
                    }
                }
            }
        }
    }
}