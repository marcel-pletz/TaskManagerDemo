namespace TaskManagerDemo.Domain;

public interface IUnitOfWork
{
    Task SaveChanges();
}