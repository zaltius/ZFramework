using ZFramework.Domain.Entities.Auditing;

namespace ZFramework.Application.Services.Dtos.Auditing
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IDeletionAudited"/> interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IDeletionAudited
    {
        /// <summary>
        /// Is this entity deleted?
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Deletion user, if this entity is deleted,
        /// </summary>
        public string? DeletionUsername { get; set; }

        /// <summary>
        /// Deletion time, if this entity is deleted,
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}