using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Domain.Todos.Repositories;

public interface ITodoRepository : IAddableRepository<Todo>, IQueryableRepository<Todo>, IRemovalRepository<Todo>
{
    public Task<Todo> GetById(TodoId id, CancellationToken cancellationToken);
    
    public IQueryable<Todo> ListTodosOwnedByUser(UserId ownerId);
}