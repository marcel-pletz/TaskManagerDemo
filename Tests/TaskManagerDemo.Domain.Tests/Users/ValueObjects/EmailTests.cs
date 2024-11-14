namespace TaskManagerDemo.Domain.Users.ValueObjects;

public class EmailTests
{
    [TestCase("test@example.com")]
    [TestCase("test~test@example.de")]
    public void Email_Is_Valid(string givenEmail)
    {
        Email email = null!; 
        Assert.That(() => email = new Email(givenEmail), Throws.Nothing);
        Assert.That(email.Value, Is.EqualTo(givenEmail));
    }
    
    [TestCase("test-invalid.com")]
    [TestCase("test-invalid")]
    public void Email_Is_Not_Valid(string givenEmail)
    {
        Assert.That(() => new Email(givenEmail), Throws.ArgumentException);
    }
}