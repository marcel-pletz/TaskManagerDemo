using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Domain.Users.Repositories;

public interface IUserRepository : IQueryableRepository<User>, IAddableRepository<User>
{
    Task<User> GetById(UserId userId, CancellationToken cancellationToken);
    
    Task<User> GetByUsername(UserName userName, CancellationToken cancellationToken);
}