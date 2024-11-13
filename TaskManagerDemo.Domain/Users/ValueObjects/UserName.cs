using System.Text.RegularExpressions;

namespace TaskManagerDemo.Domain.Users.ValueObjects;

public sealed record UserName
{
    private static readonly Regex ValidUserNamePattern = new(@"^[A-Za-z0-9]+$", RegexOptions.IgnoreCase);
    
    public readonly string Value;
    public UserName(string userName)
    {
        Validate(userName);
        Value = userName;
    }
    
    private static void Validate(string userName)
    {
        if (userName.Contains(' '))
        {
            throw new ArgumentException($"No whitespaces allowed: {userName}");
        }

        if (!ValidUserNamePattern.IsMatch(userName))
        {
            throw new ArgumentException($"Only alphanumeric characters allowed: {userName}");    
        }
    }
}