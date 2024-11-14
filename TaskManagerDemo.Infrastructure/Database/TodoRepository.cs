using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Infrastructure.Database;

public class TodoRepository(TaskManagerDbContext context) : ITodoRepository
{
    public void Add(Todo todo)
    {
        context.Todos.Add(todo);
    }

    public void Remove(Todo todo)
    {
        context.Todos.Remove(todo);
    }
    
    public Task<Todo> GetById(TodoId id, CancellationToken cancellationToken)
    {
        return context.Todos
            .Where(x => x.Id == id)
            .SingleAsync(cancellationToken);
    }

    public Task<Todo[]> ListTodosOwnedByUser(UserId ownerId, CancellationToken cancellationToken)
    {
        return context.Todos
            .Where(x => x.OwnerId == ownerId)
            .ToArrayAsync(cancellationToken);
    }

    public IQueryable<Todo> Query()
    {
        return context.Todos;
    }
}