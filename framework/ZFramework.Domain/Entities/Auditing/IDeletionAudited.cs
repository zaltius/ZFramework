using ZFramework.Domain.Entities.Authentication;

namespace ZFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store deletion information (when was deleted).
    /// Properties are automatically set when deleting the <see cref="IEntity"/>.
    /// </summary>
    public interface IDeletionAudited : ISoftDelete
    {
        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        DateTime? DeletionTime { get; set; }

        /// <summary>
        /// Deletion user name of the entity.
        /// </summary>
        string? DeletionUsername { get; set; }
    }

    /// <summary>
    /// Contract used to audit deletion-related actions (who and when deleted the entity).
    /// </summary>
    /// <typeparam name="TPrimaryKeyUser">Primary Key of the user who interact with the entity.</typeparam>
    /// <typeparam name="TUser">Represents the type of the user who interact with the entity.</typeparam>
    public interface IDeletionAudited<TPrimaryKeyUser, TUser> : IDeletionAudited
        where TUser : class, IUser
    {
        /// <summary>
        /// Primary key of <see cref="DeletionUser"/>.
        /// </summary>
        TPrimaryKeyUser DeletionUserId { get; set; }

        /// <summary>
        /// Deletion user of the entity.
        /// </summary>
        TUser DeletionUser { get; set; }
    }
}