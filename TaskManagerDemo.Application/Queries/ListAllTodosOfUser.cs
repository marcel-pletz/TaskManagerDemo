﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Application.Queries;

public sealed record ListAllTodosOfUser() : IRequest<ListAllTodosOfUser.TodoListDto>
{
    public sealed class Handler(ITodoRepository todoRepository, IUserProvider userProvider) : IRequestHandler<ListAllTodosOfUser, TodoListDto>
    {
        public async Task<TodoListDto> Handle(ListAllTodosOfUser request, CancellationToken cancellationToken)
        {
            var user = await userProvider.ProvideCurrentUser(cancellationToken);

            var entries = await todoRepository.ListTodosOwnedByUser(user.Id)
                .Select(x => new TodoEntryDto
                {
                    Id = x.Id.Value,
                    Title = x.Title.Value,
                    DueDate = x.DueDate!.Value,
                    Status = x.Status,
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

        public required Status Status { get; set; }
        
        public DateOnly? DueDate { get; init; }
    }
}