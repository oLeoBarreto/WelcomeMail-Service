using RabbitMQ.Client;

namespace WelcomeMail_Service.Configs.RabbitMQ;

public class RabbitMQFactory()
{
    private readonly RabbitMQProperties? _properties;

    public RabbitMQFactory(RabbitMQProperties properties) : this()
    {
        _properties = properties;
    }

    public IModel CreteConnection()
    {
        var factory = new ConnectionFactory() { HostName = _properties.Host};
        var connection = factory.CreateConnection();
        var chanel = connection.CreateModel();

        chanel.ExchangeDeclare(_properties.ExchangeName,_properties.ExchangeType);
        chanel.QueueDeclare(_properties.QueueName);
        chanel.QueueBind(_properties.QueueName, _properties.ExchangeName, _properties.RoutingKey);

        return chanel;
    }
}