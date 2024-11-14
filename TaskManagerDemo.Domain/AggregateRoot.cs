namespace TaskManagerDemo.Domain;

public abstract class AggregateRoot : IAggregateRoot
{
    private readonly List<DomainEvent> domainEvents = [];

    public IReadOnlyCollection<DomainEvent> DomainEvents => domainEvents;
    
    protected void Raise(DomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
}