using MediatR;
using Microsoft.AspNetCore.Http;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Todos.Services;
using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Application.Queries;

public sealed record GetTodo(string Id) : IRequest<GetTodo.TodoDto>
{
    public sealed class Handler(ITodoRepository todoRepository, TodoPermissionGuard guard, IUserProvider userProvider) : IRequestHandler<GetTodo, TodoDto>
    {
        public async Task<TodoDto> Handle(GetTodo request, CancellationToken cancellationToken)
        {
            var user = await userProvider.ProvideCurrentUser(cancellationToken);
            var todo = await todoRepository.GetById(request.Id, cancellationToken);
            
            guard.GuardAccessToTodo(user, todo);
            
            return Map(todo);
        }

        private TodoDto Map(Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id.Value,
                Title = todo.Title.Value,
                Description = todo.Description.Value,
                Status = todo.Status,
                DueDate = todo.DueDate
            };
        }
    }
    
    public sealed record TodoDto
    {
        public required Guid Id { get; init; }
        
        public required string Title { get; init; }
        
        public required string Description { get; init; }
        
        public required Status Status { get; set; }
        
        public DateOnly? DueDate { get; init; }
    }
}