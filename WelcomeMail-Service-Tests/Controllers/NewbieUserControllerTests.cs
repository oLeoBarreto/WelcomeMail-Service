using WelcomeMail_Service.Controllers;
using WelcomeMail_Service.Model;
using WelcomeMail_Service.Services;

namespace WelcomeMail_Service_Tests.Controllers;

public class NewbieUserControllerTests
{
    [Fact]
    public void IsPossibleToPostANewbieUser()
    {
        var user = new User("test@test.com", "User Test");
        var controller = new NewbieUserController();
        
        Assert.True(controller.NewUserRegistered(user).IsCompleted);
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