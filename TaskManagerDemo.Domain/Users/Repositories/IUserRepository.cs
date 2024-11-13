using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Domain.Users.Repositories;

public interface IUserRepository : IQueryableRepository<User>, IAddableRepository<User>
{
    Task<User> GetUserById(UserId userId);
}