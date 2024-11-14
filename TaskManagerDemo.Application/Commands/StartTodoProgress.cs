using MediatR;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Services;

namespace TaskManagerDemo.Application.Commands;

public sealed record StartTodoProgress(string Id) : ModifyTodoRequest(Id)
{
    public sealed class Handler(IUnitOfWork unitOfWork, 
        TodoPermissionGuard guard, 
        IUserProvider userProvider, 
        TimeProvider timeProvider) 
        : HandlerTemplate<StartTodoProgress>(unitOfWork, userProvider, guard)
    {
        protected override void DoModification(Todo todo, StartTodoProgress request)
        {
            todo.StartProgress(timeProvider.GetUtcNow().DateTime);
        }
    }
}