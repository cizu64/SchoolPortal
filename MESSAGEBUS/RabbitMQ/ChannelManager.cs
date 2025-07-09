using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MESSAGEBUS.RabbitMQ;

public class ChannelManager : IDisposable
{
    private readonly IChannel _channel;
    

    public ChannelManager(IChannel channel)
    {
        _channel = channel;
    }
    public async Task QueueBind(string queue, string exchange, string routingKey)
    {
        await _channel.QueueBindAsync(queue, exchange, routingKey);
    }
    public async Task BasicConsumeAsync(string queue, IAsyncBasicConsumer consumer, bool autoAck = false)
    {
        await _channel.BasicConsumeAsync(queue, autoAck, consumer);
    }

    public async void Dispose()
    {
        await _channel.DisposeAsync();
    }

}