
using MESSAGEBUS.RabbitMQ;
using MICROSERVICE_ONE.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MICROSERVICE_ONE.HostedService;

public class IntegrationOutboxProcessor : BackgroundService
{
    private readonly ServiceCollection sc;
    private IConfiguration _configuration;
    private readonly MicroContext context;
    public IntegrationOutboxProcessor(IConfiguration configuration)
    {
        _configuration = configuration;
        sc = new ServiceCollection();
        sc.AddDbContext<MicroContext>(option =>
        {
            option.UseNpgsql(_configuration["ConnectionString"]);
        });
        var provider = sc.BuildServiceProvider();
        context = provider.GetRequiredService<MicroContext>();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        while (await timer.WaitForNextTickAsync())
        {
            try
            {
                var integrations = context.IntegrationData.Where(i => !i.IsPublished && i.RetryCount != i.MaxRetryCount);

                foreach (var data in integrations)
                {
                    bool isPublished = await Publisher.Send(data.Body, data.QueueName, int.Parse(_configuration["RabbitMQ:Port"]), _configuration["RabbitMQ:Username"], _configuration["RabbitMQ:Password"], _configuration["RabbitMQ:Host"]);
                    data.SetRetryCount(data.RetryCount + 1);
                    data.SetPublished(isPublished);
                }

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
        }
    }
}