using ZFramework.Common.Authentication;

namespace ZFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// Prevents mocking, faking or modifiying of audit-related properties.
    /// </summary>
    public static class AuditPropertiesHelper
    {
        /// <summary>
        /// Checks and corrects, if necessary, the auditing properties representing a deletion state of newly created entities.
        /// I.e., an entity implementing <see cref=" IDeletionAudited"/> with property <see cref="ISoftDelete.IsDeleted"/> set o false but with property <see cref=" IDeletionAudited.DeletionTime"/> set to a non-null value.
        /// In this case, property <see cref=" IDeletionAudited.DeletionTime"/> would be set to null to correct the introduced mistake.
        /// </summary>
        /// <param name="entityAsObject">Entity to evaluate casted as an objetc.</param>
        /// <param name="deletionTime">Deletion time of the entity. Must be provided for just in case.</param>
        /// <param name="deletionUsername">Deletion username of the entity. Must be provided for just in case.</param>
        public static void CorrectAddedDeletionAuditProperties(object entityAsObject, DateTime deletionTime, string? deletionUsername)
        {
            if (entityAsObject is IDeletionAudited auditedEntity)
            {
                if (auditedEntity.IsDeleted)
                {
                    auditedEntity.DeletionTime = deletionTime;
                    auditedEntity.DeletionUsername = deletionUsername;
                }
                else
                {
                    auditedEntity.DeletionTime = null;
                    auditedEntity.DeletionUsername = null;
                }
            }
        }

        /// <summary>
        /// Checks and corrects, if necessary, the modification-related auditing properties of modified entities.
        /// </summary>
        /// <param name="entityAsObject">Entity to evaluate casted as an objetc.</param>
        public static void CorrectModificationAuditProperties(object entityAsObject)
        {
            if (entityAsObject is IModificationAudited auditedEntity)
            {
                auditedEntity.LastModificationTime = null;
            }
        }

        /// <summary>
        /// Sets all creation-related auditing properties of newly created entities.
        /// </summary>
        /// <param name="entityAsObject">Entity to evaluate casted as an objetc.</param>
        /// <param name="creationTime">>Creation time of the entity.</param>
        /// <param name="creationUsername">>Creation username of the entity.</param>
        public static void SetCreationAuditProperties(object entityAsObject, DateTime creationTime, string creationUsername)
        {
            if (entityAsObject is ICreationAudited auditedEntity)
            {
                auditedEntity.CreationTime = creationTime;
                auditedEntity.CreationUsername = creationUsername;
            }
        }

        /// <summary>
        /// Sets all modification-related auditing properties of modified entities.
        /// </summary>
        /// <param name="entityAsObject">Entity to evaluate casted as an objetc.</param>
        /// <param name="lastModificationTime">Last modification time of the entity.</param>
        /// <param name="lastModificationUsername">Last modification username of the entity.</param>
        public static void SetModificationAuditProperties(object entityAsObject, DateTime lastModificationTime, string? lastModificationUsername)
        {
            if (entityAsObject is IModificationAudited auditedEntity)
            {
                auditedEntity.LastModificationTime = lastModificationTime;
                auditedEntity.LastModificationUsername = lastModificationUsername;
            }
        }
    }
}