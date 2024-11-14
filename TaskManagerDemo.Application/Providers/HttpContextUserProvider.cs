using Microsoft.AspNetCore.Http;
using TaskManagerDemo.Application.Extensions;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.Repositories;

namespace TaskManagerDemo.Application.Providers;

public class HttpContextUserProvider(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : IUserProvider
{
    public Task<User> ProvideCurrentUser(CancellationToken cancellationToken)
    {
        var username = httpContextAccessor.HttpContext!.User.Identity!.Name!;
        
        return userRepository.GetByUsername(username, cancellationToken);
    }
}