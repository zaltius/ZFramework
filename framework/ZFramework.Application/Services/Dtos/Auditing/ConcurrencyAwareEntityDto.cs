using ZFramework.Domain.Entities.Auditing;

namespace ZFramework.Application.Services.Dtos.Auditing
{
    /// <summary>
    /// This DTO is used to check wether an entity has been modified since it was requested.
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class ConcurrencyAwareEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>, IModificationAudited
    {
        /// <summary>
        /// Last modification time (UTC) of the entity when it was requested.
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        public string? LastModificationUsername { get; set; }
    }
}