using ZFramework.Domain.Entities.Auditing;

namespace ZFramework.Application.Services.Dtos.Auditing
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IModificationAudited{TUser}"/> interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IModificationAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modification user of this entity.
        /// </summary>
        public string LastModificationUsername { get; set; }
    }
}