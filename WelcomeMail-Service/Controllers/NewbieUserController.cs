using Microsoft.AspNetCore.Mvc;
using WelcomeMail_Service.Handlers;
using WelcomeMail_Service.Model;
using WelcomeMail_Service.Services;

namespace WelcomeMail_Service.Controllers;

[ApiController]
[Route("NewbieUsers")]
public class NewbieUserController : ControllerBase
{
    private readonly MailSender _mailSender = new("", "");

    [HttpPost(Name = "SendWelcomeEmailToNewbieUsers")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(EmailNotSentException), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<string> NewUserRegistered(User user)
    {
        await _mailSender.sendEmailAsync(user.email, "Welcome to our application!", "Thank you for joining our application. We are excited to have you on board.");
        return "Email sent successfully to " + user.name;
    }
}