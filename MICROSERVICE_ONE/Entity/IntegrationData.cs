namespace MICROSERVICE_ONE.Entity;

public class IntegrationData
{
    public IntegrationData(string body, string queueName)
    {
        Body = body;
        QueueName = queueName;
    }

    public Guid Id { get; private set; }
    public string Body { get; private set; }
    public string QueueName { get; private set; }
    public bool IsPublished { get; private set; }
    public int RetryCount { get; private set; } = 0;
    public int MaxRetryCount { get; private set; } = 3;

    public void SetPublished(bool isPublished)
    {
        IsPublished = isPublished;
    }
    public void SetRetryCount(int retryCount)
    {
        RetryCount = retryCount;
    }
}