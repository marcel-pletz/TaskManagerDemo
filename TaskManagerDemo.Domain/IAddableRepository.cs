namespace TaskManagerDemo.Domain;

public interface IAddableRepository<in TAggregate> where TAggregate : IAggregateRoot
{
    Task Add(TAggregate aggregate, CancellationToken cancellationToken);
}