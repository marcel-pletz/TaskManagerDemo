using MediatR;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Services;

namespace TaskManagerDemo.Application.Commands;

public sealed record DeleteTodo(string Id) : ModifyTodoRequest(Id)
{
    public sealed class Handler(IUnitOfWork unitOfWork, 
        IUserProvider userProvider,
        TodoPermissionGuard guard,
        IMediator mediator) 
        : HandlerTemplate<DeleteTodo>(unitOfWork, userProvider, guard)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        protected override void DoModification(Todo todo, DeleteTodo request)
        {
            _unitOfWork.TodoRepository.Remove(todo);
        }
    }
}