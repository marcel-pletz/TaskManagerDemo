using MediatR;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Services;

namespace TaskManagerDemo.Application.Commands;

public abstract record ModifyTodoRequest(string Id) : IRequest
{
    public abstract class HandlerTemplate<TRequest>(
        IUnitOfWork unitOfWork, 
        IUserProvider userProvider, 
        TodoPermissionGuard guard) 
        : IRequestHandler<TRequest> where TRequest : ModifyTodoRequest
    {
        public async Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await userProvider.ProvideCurrentUser(cancellationToken);
            var todo = await unitOfWork.TodoRepository.GetById(request.Id, cancellationToken);

            guard.GuardAccessToTodo(currentUser, todo);
            
            DoModification(todo, request);
            
            await unitOfWork.SaveChanges(cancellationToken);
        }
    
        protected abstract void DoModification(Todo todo, TRequest request);
    }
}

