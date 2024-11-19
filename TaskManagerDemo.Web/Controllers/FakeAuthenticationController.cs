using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDemo.Application.Providers;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.Repositories;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Web.Controllers;

/// <summary>
/// Everything is extremely simplified and is really, really not a recommended way  
/// </summary>
/// <param name="userRepository"></param>
[Route("api/auth")]
public class FakeAuthenticationController(IUserRepository userRepository) : ControllerBase
{
    private const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    [HttpGet("current-user")]
    public async Task<ActionResult<AuthenticationDto>> GetCurrentUser([FromServices] IUserProvider userProvider, CancellationToken cancellationToken)
    {
        if (HttpContext.User.Identity?.IsAuthenticated == true)
        {
            var user = await userProvider.ProvideCurrentUser(cancellationToken);
            var userDto = AuthenticatedUserDto.From(user);

            var authenticationDto = new AuthenticationDto(IsAuthenticated: true)
            {
                User = userDto
            };
            return Ok(authenticationDto);
        }
        else
        {
            var authenticationDto = new AuthenticationDto(false);
            return Ok((authenticationDto));    
        }
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromQuery] string username, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsername(new UserName(username), cancellationToken);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username.Value),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        
        await HttpContext.SignInAsync(AuthenticationScheme, principal, new AuthenticationProperties());
        
        return Ok();
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(AuthenticationScheme);
        
        return Ok();
    }

    public record struct AuthenticationDto(bool IsAuthenticated)
    {
        public AuthenticatedUserDto? User { get; init; }
    }
    
    public record struct AuthenticatedUserDto
    {
        public string UserName { get; private init; }
        
        public Role Role { get; private init; }

        internal static AuthenticatedUserDto From(User user)
        {
            return new AuthenticatedUserDto
            {
                UserName = user.Username.Value,
                Role = user.Role,
            };
        }
    }
}