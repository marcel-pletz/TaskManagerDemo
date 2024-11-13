namespace TaskManagerDemo.Domain.Todos.ValueObjects;

public class TitleTests
{
    [TestCase("")]
    [TestCase("  ")]

    public void Title_Must_Not_Empty_Or_Whitespace(string givenTitle)
    {
        Assert.That(() => new Title(givenTitle), Throws.ArgumentException);
    }
    
    [TestCase(101)]
    [TestCase(200)]
    public void Title_Must_Not_Be_Longer_Than_100_Characters(int titleLength)
    {
        var givenString = CreateTitleWithLength(titleLength);

        Assert.That(() => new Title(givenString), Throws.ArgumentException);
    }

    private string CreateTitleWithLength(int titleLength)
    {
        var chars = Enumerable.Repeat('a', titleLength).ToArray();
        return new string(chars);
    }

    [TestCase(1)]
    [TestCase(100)]
    
    public void Title_Might_Be_Shorter_Than_Or_Equal_100_Characters(int titleLength)
    {
        var givenString = CreateTitleWithLength(titleLength);
        Assert.That(() => new Title(givenString), Throws.Nothing);
    }
}