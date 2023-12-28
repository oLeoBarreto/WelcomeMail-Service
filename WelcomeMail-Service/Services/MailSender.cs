using System.Net;
using WelcomeMail_Service.Handlers;

namespace WelcomeMail_Service.Services;

using System.Net.Mail;

public class MailSender : IMailSender
{
    private string fromMail;
    private string fromPassword;

    public MailSender(string fromMail, string fromPassword)
    {
        this.fromMail = fromMail;
        this.fromPassword = fromPassword;
    }

    public async Task sendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MailMessage();

        message.From = new MailAddress(this.fromMail);
        message.Subject = subject;
        message.To.Add(new MailAddress(email));
        message.Body = "<html><body>" + htmlMessage + "</body></html>";
        message.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.Credentials = new NetworkCredential(this.fromMail, this.fromPassword);
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