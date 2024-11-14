using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Domain.Todos.Services;

public sealed class TodoPermissionGuard
{
    public void GuardAccessToTodo(User user, Todo todo)
    {
        if (!CanUserAccessTodo(user, todo))
        {
            throw new UnauthorizedAccessException($"User {user.Username.Value} cannot access {todo.Id.Value}");
        }
    }

    private bool CanUserAccessTodo(User user, Todo todo)
    {
        if (todo.IsOwnedBy(user))
        {
            return true;
        }

        return user.Role == Role.Administrator;
    }
}