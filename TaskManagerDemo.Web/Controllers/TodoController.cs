using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDemo.Application.Commands;
using TaskManagerDemo.Application.Queries;

namespace TaskManagerDemo.Web.Controllers;

[Route("api/todos")]
public sealed class TodoController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ListAllTodosOfUser.TodoListDto>> ListAllTodosOfUser(
        [FromQuery] string currentUserId,
        CancellationToken cancellationToken)
    {
        var listQuery = new ListAllTodosOfUser(currentUserId);
        
        var list = await mediator.Send(listQuery, cancellationToken);

        return Ok(list);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetTodo.TodoDto>> GetTodo(string id, [FromQuery] string currentUserId, CancellationToken cancellationToken)
    {
        var todoQuery = new GetTodo(id, currentUserId);

        var todo = await mediator.Send(todoQuery, cancellationToken);

        return Ok(todo);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromQuery]string currentUserId, [FromBody] TodoCommandDto newTodo, CancellationToken cancellationToken)
    {
        var createRequest = new CreateTodo(newTodo.Title, newTodo.Description, currentUserId)
        {
            CurrentUserId = currentUserId
        };
           
        await mediator.Send(createRequest, cancellationToken);

        return Ok();
    }
    
    [HttpPut("{id}")] 
    public async Task<ActionResult> UpdateTodo(string id, [FromQuery]string currentUserId, [FromBody] TodoCommandDto updatedTodo, CancellationToken cancellationToken)
    {
        var updateRequest = new UpdateTodo(id, updatedTodo.Title, updatedTodo.Description, currentUserId)
        {
            DueDate = updatedTodo.DueDate,
        };
           
        await mediator.Send(updateRequest, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id, [FromQuery]string currentUserId, CancellationToken cancellationToken)
    {
        var deleteRequest = new DeleteTodo(id, currentUserId);

        await mediator.Send(deleteRequest, cancellationToken);

        return Ok();
    }
    
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public sealed class TodoCommandDto
    {
        public required string Title { get; init; }
        
        public required string Description { get; init; }
        
        public DateOnly? DueDate { get; init; }
    }
}