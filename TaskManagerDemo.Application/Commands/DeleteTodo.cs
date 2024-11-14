using MediatR;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Services;

namespace TaskManagerDemo.Application.Commands;

public sealed record DeleteTodo(string Id, string CurrentUserId) : IRequest
{
    public sealed class Handler(IUnitOfWork unitOfWork, TodoPermissionGuard todoPermissionGuard) : IRequestHandler<DeleteTodo>
    {
        public async Task Handle(DeleteTodo request, CancellationToken cancellationToken)
        {
            var todo = await unitOfWork.TodoRepository.GetById(request.Id, cancellationToken);
            var currentUser = await unitOfWork.UserRepository.GetById(request.CurrentUserId, cancellationToken);
            
            todoPermissionGuard.GuardAccessToTodo(currentUser, todo);
            
            unitOfWork.TodoRepository.Remove(todo);
            
            await unitOfWork.SaveChanges(cancellationToken);
        }
    }
}