using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Users.Repositories;

namespace TaskManagerDemo.Domain;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    
    public ITodoRepository TodoRepository { get; }
    
    Task SaveChanges(CancellationToken cancellationToken);
}