using MediatR;

namespace ZFramework.Domain.Events
{
    /// <summary>
    /// Represents an event raised by the domain.
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}