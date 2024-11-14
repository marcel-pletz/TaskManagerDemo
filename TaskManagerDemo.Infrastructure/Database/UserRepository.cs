using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.Repositories;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Infrastructure.Database;

public sealed class UserRepository(TaskManagerDbContext context) : IUserRepository
{
    public IQueryable<User> Query()
    {
        return context.Users;
    }

    public void Add(User user)
    {
        context.Users.Add(user);
    }

    public Task<User> GetById(UserId userId, CancellationToken cancellationToken)
    {
        return context.Users.Where(x => x.Id == userId).SingleAsync(cancellationToken);
    }

    public Task<User> GetByUsername(UserName userName, CancellationToken cancellationToken)
    {
        return context.Users.Where(x => x.Username == userName).SingleAsync(cancellationToken);
    }
}