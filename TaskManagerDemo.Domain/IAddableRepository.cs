namespace TaskManagerDemo.Domain;

public interface IAddableRepository<in TAggregate> where TAggregate : IAggregateRoot
{
    void Add(TAggregate aggregate);
}