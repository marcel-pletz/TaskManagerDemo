using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.Repositories;

namespace TaskManagerDemo.Infrastructure.Database;

public sealed class UserRepository(TaskManagerDbContext context) : IUserRepository
{
    public IQueryable<User> Query()
    {
        return context.Users;
    }

    public async Task Add(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }

    public Task<User> GetUserById(UserId userId, CancellationToken cancellationToken)
    {
        return context.Users.Where(x => x.Id == userId).SingleAsync(cancellationToken);
    }
}