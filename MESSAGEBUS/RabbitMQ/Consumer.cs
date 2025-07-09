using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MESSAGEBUS.RabbitMQ;

public class Consumer
{
    public static async Task<AsyncEventingBasicConsumer> BasicConsume(string host, int port, string username, string password, string queue)
    {
        var connection = await ConnectionManager.CreateConnection(host, port, username, password);
        var channel = await connection.CreateChannelAsync();
        var channelManager = new ChannelManager(channel);

        await channel.QueueDeclareAsync(queue, true, false, false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        await channelManager.BasicConsumeAsync(queue,consumer);
        return consumer;
    }
}
