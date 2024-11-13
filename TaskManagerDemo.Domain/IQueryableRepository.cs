namespace TaskManagerDemo.Domain;

public interface IQueryableRepository<out TAggregate> where TAggregate : IAggregateRoot
{
    IQueryable<TAggregate> Query();
}