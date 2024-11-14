using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
}