using ZFramework.Domain.Entities.Authentication;

namespace ZFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (when was modified last).
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// </summary>
    public interface IModificationAudited
    {
        /// <summary>
        /// The last modification time for this entity.
        /// </summary>
        DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modification user name of the entity.
        /// </summary>
        string? LastModificationUsername { get; set; }
    }

    /// <summary>
    /// Contract used to audit modification-related actions (who and when modified lastly).
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKeyUser">Primary key of the user who interacts with the entity.</typeparam>
    /// <typeparam name="TUser">Represents the type of the user who interacts with the entity.</typeparam>
    public interface IModificationAudited<TPrimaryKeyUser, TUser> : IModificationAudited
        where TUser : class, IUser
    {
        /// <summary>
        /// Primary key of the <see cref="LastModificationUser"/>.
        /// </summary>
        TPrimaryKeyUser LastModificationUserId { get; set; }

        /// <summary>
        /// Last modification user of the entity.
        /// </summary>
        TUser LastModificationUser { get; set; }
    }
}