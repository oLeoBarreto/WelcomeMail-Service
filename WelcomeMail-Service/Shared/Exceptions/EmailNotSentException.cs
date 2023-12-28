using WelcomeMail_Service.Services;

namespace WelcomeMail_Service.Handlers;

[Serializable]
public class EmailNotSentException(string message, string details)
    : CustomException(message, details, 400, typeof(MailSender).ToString());