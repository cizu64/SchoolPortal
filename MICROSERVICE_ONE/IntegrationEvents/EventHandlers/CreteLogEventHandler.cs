using System.Text.Json;
using MediatR;
using MESSAGEBUS.RabbitMQ;
using MICROSERVICE_ONE.Entity;
using MICROSERVICE_ONE.Infrastructure;
using MICROSERVICE_ONE.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MICROSERVICE_ONE.IntegrationEvents.EventHandlers;

public class CreateLogEventHandler : INotificationHandler<CreateLog>
{
    private readonly MicroContext context;
    private readonly IConfiguration _configuration;
    public CreateLogEventHandler(MicroContext context, IConfiguration configuration)
    {
        this.context = context;
        _configuration = configuration;
    }
    public async Task Handle(CreateLog notification, CancellationToken cancellationToken)
    {
        EntityEntry<Log> entry = context.Add(new Log(notification.Level, notification.Message, notification.StackTrace));

        string body = JsonSerializer.Serialize(entry.Entity);

        //add integraion event data
        EntityEntry<IntegrationData> integrationData = context.Add(new IntegrationData(body, "LOGQUEUE"));
        await context.SaveChangesAsync();

        //PUBLISH MESSAGE 

        bool isPublished = await Publisher.Send(body, "LOGQUEUE", int.Parse(_configuration["RabbitMQ:Port"]), _configuration["RabbitMQ:Username"], _configuration["RabbitMQ:Password"], _configuration["RabbitMQ:Host"]);

        integrationData.Entity.SetPublished(isPublished);

    }
}