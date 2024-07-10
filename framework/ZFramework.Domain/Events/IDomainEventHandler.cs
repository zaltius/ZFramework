using MediatR;

namespace ZFramework.Domain.Events
{
    /// <summary>
    /// Contract that represents a domain event handler.
    /// </summary>
    /// <typeparam name="TDomainEvent">Represents the domain event to handle.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : class, IDomainEvent
    {
    }
}