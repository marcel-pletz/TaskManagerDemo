using System.Text.RegularExpressions;

namespace TaskManagerDemo.Domain.Users.ValueObjects;

public sealed record Email
{
    private static readonly Regex ValidEmailPattern = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    
    public readonly string Value;
    public Email(string value)
    {
        Validate(value);
        Value = value;
    }
    
    private static void Validate(string email)
    {
        
        if (!ValidEmailPattern.IsMatch(email))
        {
            throw new ArgumentException($"Invalid email {email}");
        }
    }
}