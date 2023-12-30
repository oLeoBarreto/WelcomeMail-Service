using WelcomeMail_Service.Handlers;
using WelcomeMail_Service.Services;

namespace WelcomeMail_Service_Tests.Services;

public class MailSenderTests
{
    [Fact]
    public async void IsPossibleToSendEmail()
    {
        var mailSender = new MailSender("testFrom@test.com", "12345");
        
        Assert.IsNotType<EmailNotSentException>(async () => mailSender.sendEmailAsync("testTo@test.com", "Test message", "Test message"));
    }

    [Fact]
    public void IsNotPossibleToSendMail_WithWrongAddress()
    {
        var mailSender = new MailSender("testFrom@test.com", "12345");

        Assert.ThrowsAsync<EmailNotSentException>(async () => await mailSender.sendEmailAsync("testTo", "Test message", "Test message"));
    }
}