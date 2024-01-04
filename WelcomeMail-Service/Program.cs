using Microsoft.Extensions.Options;
using WelcomeMail_Service.Configs.RabbitMQ;
using WelcomeMail_Service.Middleware;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        
        var configuration = builder.Configuration;

        if (Convert.ToBoolean(configuration.GetSection("UseRabbitMQService").Value))
        {
            services.Configure<RabbitMQProperties>(configuration.GetSection("RabbitMQProperties"));
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<RabbitMQProperties>>().Value);
            var serviceProvider = services.BuildServiceProvider();
            
            Task.Run(() =>
            {
                var rabbitMqProperties = serviceProvider.GetRequiredService<RabbitMQProperties>();
                var consumer = new RabbitMQConsumer(rabbitMqProperties);
                consumer.CreateConsumer();
            });   
        }
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMiddleware<CustomMiddleware>();
        }
        
        app.MapControllers();

        app.UseHttpsRedirection();
        
        app.Run();
    }
}