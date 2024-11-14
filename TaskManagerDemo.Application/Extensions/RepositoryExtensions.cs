using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.Repositories;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Application.Extensions;

public static class RepositoryExtensions
{
    public static Task<User> GetById(this IUserRepository repository, string id, CancellationToken cancellationToken)
    {
        var userId = UserId.From(id);
        return repository.GetById(userId, cancellationToken);
    }

    public static Task<User> GetByUsername(this IUserRepository repository, string username, CancellationToken cancellationToken)
    {
        var name = new UserName(username);
        
        return repository.GetByUsername(name, cancellationToken);
    }
    
    public static Task<Todo> GetById(this ITodoRepository repository, string id,
        CancellationToken cancellationToken)
    {
        var todoId = TodoId.From(id);
        return repository.GetById(todoId, cancellationToken);
    } 
}