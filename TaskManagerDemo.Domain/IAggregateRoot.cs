namespace TaskManagerDemo.Domain;

public interface IAggregateRoot : IEntity {
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    
    void ClearDomainEvents();
}