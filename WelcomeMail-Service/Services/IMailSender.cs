namespace WelcomeMail_Service.Services;

public interface IMailSender
{
    Task sendEmailAsync(string email, string subject, string htmlMessage);
}