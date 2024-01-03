using System.Net;
using WelcomeMail_Service.Handlers;
using System.Net.Mail;

namespace WelcomeMail_Service.Services;

public class MailSender : IMailSender
{
    private readonly string? _mailFrom;
    private readonly string? _mailFromPassword;

    public MailSender(string mailFrom, string mailFromPassword)
    {
        _mailFrom = mailFrom;
        _mailFromPassword = mailFromPassword;
    }

    public MailSender()
    {
        DotNetEnv.Env.Load();
        _mailFrom = Environment.GetEnvironmentVariable("MAIL_FROM");
        _mailFromPassword = Environment.GetEnvironmentVariable("MAIL_FROM_PASSWORD");
    }

    public async Task sendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MailMessage();

        message.From = new MailAddress(_mailFrom);
        message.Subject = subject;
        message.To.Add(new MailAddress(email));
        message.Body = "<html><body>" + htmlMessage + "</body></html>";
        message.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.Credentials = new NetworkCredential(_mailFrom, _mailFromPassword);
        smtpClient.EnableSsl = true;

        try
        {
            await smtpClient.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            throw new EmailNotSentException("E-mail not sent correctly", ex.Message);
        }
    }
}