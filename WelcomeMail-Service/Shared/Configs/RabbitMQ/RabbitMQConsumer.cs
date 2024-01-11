using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WelcomeMail_Service.Model;
using WelcomeMail_Service.Services;

namespace WelcomeMail_Service.Configs.RabbitMQ;

public class RabbitMQConsumer(RabbitMQProperties properties)
{
    private readonly RabbitMQFactory _factory = new RabbitMQFactory(properties);
    private readonly MailSender _mailSender = new MailSender();

    public void CreateConsumer()
    {
        var channel = _factory.CreteConnection();
        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += async (model, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            
            Console.WriteLine("========== New message received from rabbitmq ==========");
            Console.WriteLine($"Received message: {contentString}");

            try
            {
                var message = JsonConvert.DeserializeObject<User>(contentString);
                await _mailSender.sendEmailAsync(message.Email, $"Welcome {message.Name} to our application!",
                    "Thank you for joining our application. We are excited to have you on board.");
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine("Error deserializing JSON. Invalid format.");
                Console.WriteLine($"Exception details: {ex.Message}");
            }
        };

        channel.BasicConsume(queue: properties.QueueName, autoAck: true, consumer: consumer);
        Console.ReadKey();
    }
}