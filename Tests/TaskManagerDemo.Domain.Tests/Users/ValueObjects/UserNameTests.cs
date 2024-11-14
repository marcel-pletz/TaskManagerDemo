namespace TaskManagerDemo.Domain.Users.ValueObjects;

public class UserNameTests
{

    [TestCase("valid")]
    [TestCase("valid1")]
    public void User_Name_Is_Valid(string givenUserName)
    {
        UserName userName = null!;
        Assert.That(() => userName = new UserName(givenUserName), Throws.Nothing);
        Assert.That(userName.Value, Is.EqualTo(givenUserName));
    }
    
    [TestCase("")]
    [TestCase("  ")]
    [TestCase(" invalid ")]
    [TestCase("inva lid")]
    [TestCase("invalid!")]
    public void User_Name_Is_Invalid(string givenUserName)
    {
        Assert.That(() => new UserName(givenUserName), Throws.ArgumentException);
    }
}