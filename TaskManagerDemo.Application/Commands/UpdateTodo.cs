using MediatR;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Services;
using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Application.Commands;

public sealed record UpdateTodo(string Id, string Title, string Description, string CurrentUserId) : IRequest
{
    public DateOnly? DueDate { get; init; }
    
    public sealed class Handler(IUnitOfWork unitOfWork, TodoPermissionGuard guard, TimeProvider timeProvider) 
        : IRequestHandler<UpdateTodo>
    {
        public async Task Handle(UpdateTodo request, CancellationToken cancellationToken)
        {
            var todo = await unitOfWork.TodoRepository.GetById(request.Id, cancellationToken);
            var currentUser = await unitOfWork.UserRepository.GetById(request.CurrentUserId, cancellationToken);
            guard.GuardAccessToTodo(currentUser, todo);

            Update(todo, request);
            
            await unitOfWork.SaveChanges(cancellationToken);
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