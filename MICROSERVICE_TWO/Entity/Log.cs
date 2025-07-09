namespace MICROSERVICE_TWO.Entity;

public class Log
{
    public Log(string level, string message, string? stackTrace)
    {
        Level = level;
        Message = message;
        StackTrace = stackTrace;
    }
    public Guid Id { get; private set; }
    public string Level { get; private set; }
    public string Message { get; private set; }
    public string? StackTrace { get; private set; }
    public DateTime DateCreated { get; private set; } = DateTime.UtcNow;

}