using ZFramework.Domain.Entities.Authentication;

namespace ZFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// Implements <see cref="IDeletionAudited"/> to be a base class for full-audited entities.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IDeletionAudited
    {
        /// <summary>
        /// <inheritdoc cref="DeletionTime"/>
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// <inheritdoc cref="DeletionUsername"/>
        /// </summary>
        public virtual string? DeletionUsername { get; set; }

        /// <summary>
        /// <inheritdoc cref="IsDeleted"/>
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Implements <see cref="IDeletionAudited{TPrimaryKeyUser, TUser}"/> to be a base class for full-audited entities.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary Key of the entity.</typeparam>
    /// <typeparam name="TPrimaryKeyUser">Type of the primary key of the user who interact with the entity.</typeparam>
    /// <typeparam name="TUser">Type of the user who interact with the entity.</typeparam>
    public abstract class FullAuditedEntity<TPrimaryKey, TPrimaryKeyUser, TUser> :
        AuditedEntity<TPrimaryKey, TPrimaryKeyUser, TUser>,
        IDeletionAudited<TPrimaryKeyUser, TUser>
        where TUser : class, IUser
    {
        /// <summary>
        /// <inheritdoc cref="DeletionTime"/>
        /// </summary>
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// <inheritdoc cref="DeletionUsername"/>
        /// </summary>
        public virtual string? DeletionUsername { get; set; }

        /// <summary>
        /// <inheritdoc cref="DeletionUserId"/>
        /// </summary>
        public TPrimaryKeyUser DeletionUserId { get; set; }

        /// <summary>
        /// <inheritdoc cref="DeletionUser"/>
        /// </summary>
        public virtual TUser DeletionUser { get; set; }

        /// <summary>
        /// <inheritdoc cref="IsDeleted"/>
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}