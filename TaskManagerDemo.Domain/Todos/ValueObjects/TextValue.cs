namespace TaskManagerDemo.Domain.Todos.ValueObjects;

public abstract record TextValue
{
    public string Value { get; init; }
    
    protected abstract int MaxLength { get; }


    protected TextValue(string title)
    {
        Validate(title);
        Value = title;
    }

    private void Validate(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Title cannot be null or whitespace.", nameof(text));
        }

        if (text.Length > MaxLength)
        {
            throw new ArgumentException("Title cannot be longer than 100 characters.", nameof(text));
        }
    }
    
    public override string ToString()
    {
        return Value;
    }
    
    public static implicit operator string(TextValue id) => id.Value;
}