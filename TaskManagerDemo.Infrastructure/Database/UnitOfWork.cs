using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Users.Repositories;

namespace TaskManagerDemo.Infrastructure.Database;

public sealed class UnitOfWork (
    TaskManagerDbContext context,
    IUserRepository userRepository,
    ITodoRepository todoRepository) 
    : IUnitOfWork, IDisposable, IAsyncDisposable
{
    public IUserRepository UserRepository => userRepository;

    public ITodoRepository TodoRepository => todoRepository;

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}