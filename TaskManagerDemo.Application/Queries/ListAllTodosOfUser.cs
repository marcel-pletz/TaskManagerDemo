using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Application.Queries;

public sealed record ListAllTodosOfUser(string CurrentUserId) : IRequest<ListAllTodosOfUser.TodoListDto>
{
    public sealed class Handler(ITodoRepository repository) : IRequestHandler<ListAllTodosOfUser, TodoListDto>
    {
        public async Task<TodoListDto> Handle(ListAllTodosOfUser request, CancellationToken cancellationToken)
        {
            var currentUserId = UserId.From(request.CurrentUserId);
            var entries = await repository.Query()
                .Where(x => x.OwnerId == currentUserId)
                .Select(x => new TodoEntryDto
                {
                    Id = x.Id.Value,
                    Title = x.Title.Value,
                    DueDate = x.DueDate!.Value,
                })
                .ToArrayAsync(cancellationToken);

            return new TodoListDto(entries);
        }
    }

    public sealed record TodoListDto(TodoEntryDto[] Entries);
    
    public sealed record TodoEntryDto
    {
        public required Guid Id { get; init; }
        
        public required string Title { get; init; }
        
        public DateOnly? DueDate { get; init; }
    }
}