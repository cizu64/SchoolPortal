namespace SeedWork;
using MediatR;
public class Entity
{
    public int Id { get; private set;}
    public DateTime DateCreated { get; private set;} = DateTime.UtcNow;

    private List<INotification> _domainEvents;

     //ignore this in the entity configuration
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();


    public void  AddDomainEvents(INotification domainEvents)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(domainEvents);
    }

    public void RemoveDomainEvents(INotification domainEvents)=>_domainEvents?.Remove(domainEvents);
    public void ClearDomainEvents()=>_domainEvents?.Clear();
}