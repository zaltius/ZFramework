namespace ZFramework.Domain.Entities
{
    /// <summary>
    /// Contract for all entities.
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// Contract for all entities with primary key.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key.</typeparam>
    public interface IEntity<TPrimaryKey> : IEntity
    {
        /// <summary>
        /// Represents the primary key of the entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}