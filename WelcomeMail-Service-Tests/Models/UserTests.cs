using WelcomeMail_Service.Model;

namespace WelcomeMail_Service_Tests.Models;

public class UserTests
{
    [Fact]
    public void IsAbleToCreateUser()
    {
        var user = new User("test@email.com", "user test");
        
        Assert.NotNull(user);
        Assert.IsType<User>(user);
    }

    [Fact]
    public void IsNotAbleToCreateUser_WithWrongEmail()
    {
        Assert.Throws<FormatException>(() => new User("test", "user test"));
    }
}