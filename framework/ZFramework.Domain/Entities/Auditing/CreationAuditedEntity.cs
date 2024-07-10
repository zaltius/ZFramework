using ZFramework.Domain.Entities.Authentication;

namespace ZFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        /// <summary>
        /// <inheritdoc cref="CreationTime"/>
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// <inheritdoc cref="CreationUsername"/>
        /// </summary>
        public string CreationUsername { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected CreationAuditedEntity()
        {
            CreationTime = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited{TPrimaryKeyUser, TUser}"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity.</typeparam>
    /// <typeparam name="TPrimaryKeyUser">Type of the primary key of the user who interact with the entity.</typeparam>
    /// <typeparam name="TUser">Type of the user who interact with the entity.</typeparam>
    public abstract class CreationAuditedEntity<TPrimaryKey, TPrimaryKeyUser, TUser> :
        CreationAuditedEntity<TPrimaryKey>,
        ICreationAudited<TPrimaryKeyUser, TUser>
        where TUser : class, IUser
    {
        /// <summary>
        /// <inheritdoc cref="CreationUserId"/>
        /// </summary>
        public TPrimaryKeyUser CreationUserId { get; set; }

        /// <summary>
        /// <inheritdoc cref="CreationUser"/>
        /// </summary>
        public virtual TUser CreationUser { get; set; }
    }
}