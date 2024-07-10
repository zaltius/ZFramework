using Microsoft.EntityFrameworkCore;
using ZFramework.Domain.Entities;

namespace MediatR
{
    internal static class MediatorExtension
    {
        public static void DispatchDomainEvents(this IMediator mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.GetDomainEvents() != null && x.Entity.GetDomainEvents().Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetDomainEvents())
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                var t = mediator.Publish(domainEvent);
                if (t.Exception != null)
                    throw t.Exception;
            }
        }

        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.GetDomainEvents() != null && x.Entity.GetDomainEvents().Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetDomainEvents())
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}