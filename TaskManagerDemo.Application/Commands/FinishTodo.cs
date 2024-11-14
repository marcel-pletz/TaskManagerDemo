using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Services;

namespace TaskManagerDemo.Application.Commands;

public sealed record FinishTodo(string Id) : ModifyTodoRequest(Id)
{
    public sealed class Handler(IUnitOfWork unitOfWork, 
        TodoPermissionGuard guard, 
        IUserProvider userProvider, 
        TimeProvider timeProvider) 
        : HandlerTemplate<FinishTodo>(unitOfWork, userProvider, guard)
    {
        protected override void DoModification(Todo todo, FinishTodo request)
        {
            todo.Finish(timeProvider.GetUtcNow().DateTime);
        }
    }
}