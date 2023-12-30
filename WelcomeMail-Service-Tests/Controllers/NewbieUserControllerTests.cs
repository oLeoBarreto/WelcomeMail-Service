using WelcomeMail_Service.Controllers;
using WelcomeMail_Service.Model;

namespace WelcomeMail_Service_Tests.Controllers;

public class NewbieUserControllerTests
{
    [Fact]
    public async void IsPossibleToPostANewbieUser()
    {
        var user = new User("test@test.com", "User Test");
        var controller = new NewbieUserController();

        var result = await controller.NewUserRegistered(user);
        
        Assert.IsType<string>(result);
        Assert.Equal("Email sent successfully to " + user.Name, result);
    }

    [Fact]
    public async void IsNotPossibleToPostNewbieUser_WhenBodyIsWrong()
    {
        var user = new User("testToEmail@test.com", "User Test");
        var controller = new NewbieUserController();

        user.Email = "testToEmail";

        Assert.ThrowsAsync<FormatException>(async () => await controller.NewUserRegistered(user));
    }
}