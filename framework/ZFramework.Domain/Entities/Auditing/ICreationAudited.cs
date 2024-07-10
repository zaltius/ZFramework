using ZFramework.Domain.Entities.Authentication;

namespace ZFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store creation information (who and when created).
    /// Creation time and creation user are automatically set when saving <see cref="IEntity"/> to database.
    /// </summary>
    public interface ICreationAudited
    {
        /// <summary>
        /// Creation time (UTC) of this entity.
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Creation user name of the entity.
        /// </summary>
        string CreationUsername { get; set; }
    }

    /// <summary>
    /// Contract used to audit creation-related actions.
    /// </summary>
    /// <typeparam name="TPrimaryKeyUser">Primary key of the user who interact with the entity.</typeparam>
    /// <typeparam name="TUser">Represents the type of the user who interact with the entity.</typeparam>
    public interface ICreationAudited<TPrimaryKeyUser, TUser> : ICreationAudited
        where TUser : class, IUser
    {
        /// <summary>
        /// Primary key of <see cref="CreationUser"/>.
        /// </summary>
        TPrimaryKeyUser CreationUserId { get; set; }

        /// <summary>
        /// Creation user of the entity.
        /// </summary>
        TUser CreationUser { get; set; }
    }
}