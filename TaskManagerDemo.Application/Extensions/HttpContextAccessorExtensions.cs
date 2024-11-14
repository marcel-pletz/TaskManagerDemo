using Microsoft.AspNetCore.Http;

namespace TaskManagerDemo.Application.Extensions;

public static class HttpContextAccessorExtensions
{
    public static string GetUsername(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext!.User.Identity!.Name!;
    }
}