using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.ValueObjects;
using TaskManagerDemo.Infrastructure.Database;

namespace TaskManagerDemo.Web.Controllers;

[Route("api/users")]
public sealed class UserController(TaskManagerDbContext context) : Controller
{
    [HttpGet]
    public async Task<ActionResult<UserDto[]>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await context.Users.ToArrayAsync(cancellationToken);
        var userDtos = users.Select(UserDto.From);
        return Ok(userDtos);
    }

    public record struct UserDto
    {
        public Guid Id { get; private init; }
        
        public string UserName { get; private init; }
        
        public string Email { get; private init; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; private init; }

        internal static UserDto From(User user)
        {
            return new UserDto
            {
                Id = user.Id.Value,
                UserName = user.Username.Value,
                Email = user.Email.Value,
                Role = user.Role,
            };
        }
    }
}