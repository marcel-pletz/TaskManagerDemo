using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Domain.Todos.Repositories;

public interface ITodoRepository : IAddableRepository<Todo>
{
    public Task<Todo> GetById(TodoId id, CancellationToken cancellationToken);

    public Task<Todo[]> ListTodosOwnedByUser(UserId ownerId, CancellationToken cancellationToken);
}