namespace TaskManagerDemo.Domain.Todos.ValueObjects;

public class DescriptionTests
{
    [TestCase("")]
    [TestCase("  ")]

    public void Description_Must_Not_Empty_Or_Whitespace(string givenDescription)
    {
        Assert.That(() => new Description(givenDescription), Throws.ArgumentException);
    }
    
    [TestCase(1001)]
    [TestCase(1200)]
    public void Description_Must_Not_Be_Longer_Than_1000_Characters(int descriptionLength)
    {
        var givenString = CreateDescriptionWithLength(descriptionLength);

        Assert.That(() => new Description(givenString), Throws.ArgumentException);
    }

    private string CreateDescriptionWithLength(int descriptionLength)
    {
        var chars = Enumerable.Repeat('a', descriptionLength).ToArray();
        return new string(chars);
    }

    [TestCase(1)]
    [TestCase(1000)]
    
    public void Description_Might_Be_Shorter_Than_Or_Equal_100_Characters(int descriptionLength)
    {
        var givenString = CreateDescriptionWithLength(descriptionLength);
        Assert.That(() => new Description(givenString), Throws.Nothing);
    }
}