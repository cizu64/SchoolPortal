namespace Infrastructure;

using MediatR;
using Entities;
using System.Linq;
using SeedWork;
static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, SchoolContext context)
    {
        //fetching all the entities that has domain events
        //the changetracker track changes made in the entities in the context
        var entities = context.ChangeTracker.Entries<Entity>()
        .Where(e => e.Entity.DomainEvents!=null && e.Entity.DomainEvents.Any());

        //flatten the data to get all domain event
        var domainEvents = entities.SelectMany(x=>x.Entity.DomainEvents).ToList();

        //clearing all notifications
        entities.ToList().ForEach(e => e.Entity.ClearDomainEvents());

        foreach (INotification domainEvent in domainEvents)
            await mediator.Publish(domainEvent); //pushish the domain event. Send to multiple handlers

    }
}