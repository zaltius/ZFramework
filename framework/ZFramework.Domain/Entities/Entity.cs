using ZFramework.Domain.Events;

namespace ZFramework.Domain.Entities
{
    /// <summary>
    /// Base class for all entities with primary key.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key.</typeparam>
    public abstract class Entity<TPrimaryKey> : EntityBase, IEntity<TPrimaryKey>
    {
        /// <inheritdoc/>
        public TPrimaryKey Id { get; set; }
    }

    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class EntityBase : IEntity
    {
        private IList<IDomainEvent>? _domainEvents;

        /// <summary>
        /// Removes all domain events from the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// Gets all domain events from the entity.
        /// </summary>
        public IEnumerable<IDomainEvent> GetDomainEvents() => _domainEvents ?? new List<IDomainEvent>();

        public override string ToString()
        {
            return $"[{GetType().Name}]";
        }

        /// <summary>
        /// Attaches a new domain event to the entity.
        /// Domain events represent actions performed by or to the entity that must be notified to be listened otherwhere else.
        /// </summary>
        /// <param name="domainEvent">Domain event to add.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();

            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        /// <param name="domainEvent">Domain event to remove.</param>
        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }
    }
}