namespace TaskManagerDemo.Domain;

public interface IRemovalRepository<in TAggregate> where TAggregate : IAggregateRoot
{
    void Remove(TAggregate aggregate);
}