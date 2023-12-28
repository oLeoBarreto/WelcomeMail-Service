namespace WelcomeMail_Service.Handlers;

[Serializable]
public class CustomException : Exception
{

    public string _message;
    public string _details;
    public int _statusCode;
    public string _className;
    public DateTime _timestamp;

    public CustomException(string message, string details, int statusCode, string className)
    {
        _message = message;
        _details = details;
        _statusCode = statusCode;
        _className = className;
        _timestamp = DateTime.UtcNow;
    }

    public CustomException()
    {
    }

    public CustomException(string? message) : base(message)
    {
    }

    public CustomException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}