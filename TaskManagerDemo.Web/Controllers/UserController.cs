using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Web.Controllers;

[Route("api/users")]
public sealed class UserController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<UserDto[]>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.Query().ToListAsync(cancellationToken);
        var userDtos = users.Select(UserDto.From);
        return Ok(userDtos);
    }

    public record struct UserDto
    {
        public Guid Id { get; private init; }
        
        public string UserName { get; private init; }
        
        public string Email { get; private init; }
        
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