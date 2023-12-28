using System.Net;
using System.Text.Json;
using WelcomeMail_Service.Handlers;

namespace WelcomeMail_Service.Middleware;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string message;
        string details;
        int statusCode;
        string className;
        DateTime timestamp;
        
        var exceptionType = exception.GetType();

        if (exceptionType == typeof(CustomException) || exceptionType == typeof(EmailNotSentException))
        {
            var ex = (CustomException)exception;
            
            message = ex._message;
            details = ex._details;
            statusCode = ex._statusCode;
            className = ex._className;
            timestamp = ex._timestamp;
        }
        else
        {
            message = "Internal server error";
            details = exception.Message;
            statusCode = (int)HttpStatusCode.InternalServerError;
            className = exception.GetType().ToString();
            timestamp = DateTime.UtcNow;
        }

        var response = JsonSerializer.Serialize(new { message, details, statusCode, className, timestamp });
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(response);
    }
}