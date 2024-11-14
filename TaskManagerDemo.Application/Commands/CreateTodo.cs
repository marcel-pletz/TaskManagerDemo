using MediatR;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Application.Commands;

public record CreateTodo(string Title, string Description, string CurrentUserId) : IRequest
{
    public DateOnly? DueDate { get; init; }
    
    
    public class Handler(IUnitOfWork unitOfWork, TimeProvider timeProvider) : IRequestHandler<CreateTodo>
    {
        public async Task Handle(CreateTodo request, CancellationToken cancellationToken)
        {
            var currentUser = await unitOfWork.UserRepository.GetById(request.CurrentUserId, cancellationToken);

            var todo = CreateTodoFrom(request, currentUser); 
            
            unitOfWork.TodoRepository.Add(todo);

            await unitOfWork.SaveChanges(cancellationToken);
        }

        private Todo CreateTodoFrom(CreateTodo request, User currentUser)
        {
            var now = timeProvider.GetUtcNow().DateTime.ToLocalTime();
            var title = new Title(request.Title);
            var description = new Description(request.Description);

            return Todo.Create(title,
                description,
                ownerId: currentUser.Id,
                now,
                request.DueDate);
        }
    }
}