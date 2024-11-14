using MediatR;
using Microsoft.AspNetCore.Http;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Application.Commands;

public record CreateTodo(string Title, string Description) : IRequest
{
    public DateOnly? DueDate { get; init; }
    
    
    public sealed class Handler(IUnitOfWork unitOfWork, IUserProvider userProvider, TimeProvider timeProvider) : IRequestHandler<CreateTodo>
    {
        public async Task Handle(CreateTodo request, CancellationToken cancellationToken)
        {
            var currentUser = await userProvider.ProvideCurrentUser(cancellationToken);
            
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