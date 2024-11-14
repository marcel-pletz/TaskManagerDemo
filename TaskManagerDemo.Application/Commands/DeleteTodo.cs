using MediatR;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Services;

namespace TaskManagerDemo.Application.Commands;

public sealed record DeleteTodo(string Id) : IRequest
{
    public sealed class Handler(IUnitOfWork unitOfWork, IUserProvider userProvider, TodoPermissionGuard todoPermissionGuard) : IRequestHandler<DeleteTodo>
    {
        public async Task Handle(DeleteTodo request, CancellationToken cancellationToken)
        {
            var currentUser = await userProvider.ProvideCurrentUser(cancellationToken);
            var todo = await unitOfWork.TodoRepository.GetById(request.Id, cancellationToken);

            todoPermissionGuard.GuardAccessToTodo(currentUser, todo);
            
            unitOfWork.TodoRepository.Remove(todo);
            
            await unitOfWork.SaveChanges(cancellationToken);
        }
    }
}