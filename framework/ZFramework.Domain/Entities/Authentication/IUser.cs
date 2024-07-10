namespace ZFramework.Domain.Entities.Authentication
{
    public interface IUser : IEntity
    {
        /// <summary>
        /// Just a username.
        /// </summary>
        string UserName { get; set; }
    }

    public interface IUser<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Just a username.
        /// </summary>
        string UserName { get; set; }
    }
}