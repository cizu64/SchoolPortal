using MediatR;

namespace MICROSERVICE_ONE.IntegrationEvents.Events;

public class CreateLog:INotification
{
    public Guid Id { get;  set; }
    public string Level { get;  set; }
    public string Message { get;  set; }
    public string? StackTrace { get;  set; }
    public DateTime DateCreated { get;  set; } = DateTime.UtcNow;
}