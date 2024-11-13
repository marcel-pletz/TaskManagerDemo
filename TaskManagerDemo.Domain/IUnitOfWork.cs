using TaskManagerDemo.Domain.Users.Repositories;

namespace TaskManagerDemo.Domain;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    
    Task SaveChanges(CancellationToken cancellationToken);
}