

using System.Text;
using System.Text.Json;
using MESSAGEBUS.RabbitMQ;
using RabbitMQ.Client.Events;

namespace MICROSERVICE_TWO.HostedService;

public class LogService : BackgroundService
{
    IConfiguration _configuration;
    ILogger<LogService> _logger;
    AsyncEventingBasicConsumer consumer;
    public LogService(IConfiguration configuration, ILogger<LogService> logger)
    {
        _configuration = configuration;
        SetUp().GetAwaiter().GetResult();
        _logger = logger;
    }

    public async Task SetUp()
    {
        try
        {
            consumer = await Consumer.BasicConsume(_configuration["RabbitMQ:Host"], int.Parse(_configuration["RabbitMQ:Port"]), _configuration["RabbitMQ:Username"], _configuration["RabbitMQ:Password"], "LOGQUEUE");
            consumer.ReceivedAsync += ReceivedEventAsync;
        }
        catch (Exception ex)
        {}
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(LogService)} Background service running");
    }
    public async Task ReceivedEventAsync(object sender, BasicDeliverEventArgs e)
    {
        try
        {
            var body = e.Body.ToArray();
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(Encoding.UTF8.GetString(body));
            _logger.LogInformation(JsonSerializer.Serialize(data));
            await consumer.Channel.BasicAckAsync(e.DeliveryTag, false);
        }
        catch (Exception ex)
        {}
    }
}