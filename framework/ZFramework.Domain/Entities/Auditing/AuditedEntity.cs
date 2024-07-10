using ZFramework.Domain.Entities.Authentication;

namespace ZFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IModificationAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IModificationAudited
    {
        /// <summary>
        /// <inheritdoc cref="LastModificationTime"/>
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// <inheritdoc cref="LastModificationUsername"/>
        /// </summary>
        public virtual string? LastModificationUsername { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IModificationAudited{TPrimaryKey, TUser}"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity.</typeparam>
    /// <typeparam name="TPrimaryKeyUser">Type of the primary key of the user who interacts with the entity.</typeparam>
    /// <typeparam name="TUser">Type of the user who interacts with the entity.</typeparam>
    public abstract class AuditedEntity<TPrimaryKey, TPrimaryKeyUser, TUser> :
        CreationAuditedEntity<TPrimaryKey, TPrimaryKeyUser, TUser>,
        IModificationAudited<TPrimaryKeyUser, TUser>
        where TUser : class, IUser
    {
        /// <summary>
        /// <inheritdoc cref="LastModificationTime"/>
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// <inheritdoc cref="LastModificationUsername"/>
        /// </summary>
        public virtual string? LastModificationUsername { get; set; }

        /// <summary>
        /// <inheritdoc cref="LastModificationUserId"/>
        /// </summary>
        public TPrimaryKeyUser LastModificationUserId { get; set; }

        /// <summary>
        /// <inheritdoc cref="LastModificationUser"/>
        /// </summary>
        public virtual TUser LastModificationUser { get; set; }
    }
}