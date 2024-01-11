namespace WelcomeMail_Service.Configs.RabbitMQ;

public class RabbitMQProperties
{
    public string? Host { get; set; }
    public string? QueueName { get; set; }
    public string? ExchangeName { get; set; }
}