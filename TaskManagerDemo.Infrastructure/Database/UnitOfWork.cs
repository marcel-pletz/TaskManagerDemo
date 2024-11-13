using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Users.Repositories;

namespace TaskManagerDemo.Infrastructure.Database;

public sealed class UnitOfWork(TaskManagerDbContext context, IUserRepository userRepository) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}