using MediatR;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Services;
using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Application.Commands;

public sealed record UpdateTodo(string Id, string Title, string Description) : ModifyTodoRequest(Id)
{
    public DateOnly? DueDate { get; init; }
    
    public sealed class Handler(IUnitOfWork unitOfWork, 
        IUserProvider userProvider, 
        TodoPermissionGuard guard, 
        TimeProvider timeProvider) 
        : HandlerTemplate<UpdateTodo>(unitOfWork, userProvider, guard)
    {
        protected override void DoModification(Todo todo, UpdateTodo request)
        {
            Update(todo, request);
        }

        private void Update(Todo todo, UpdateTodo request)
        {
            var title = new Title(request.Title);
            var description = new Description(request.Description);
            
            todo.Update(title, description);
            
            UpdateDueDate(todo, request.DueDate);
        }

        private void UpdateDueDate(Todo todo, DateOnly? dueDate)
        {
            if (dueDate.HasValue)
            {
                var today = DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime);
                todo.ChangeDueDate(dueDate.Value, today);
            }
            else
            {
                todo.RemoveDueDate();
            }
        }
    }
}