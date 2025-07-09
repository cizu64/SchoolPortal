using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MESSAGEBUS.RabbitMQ;

public class Publisher
{
    public static async Task<bool> Send(string body, string queue, int port, string username, string password, string hostName = "localhost")
    {
        try
        {
            var connection = await ConnectionManager.CreateConnection(hostName, port, username, password);
            var channel = await connection.CreateChannelAsync();
            var channelManager = new ChannelManager(channel);

            var props = new BasicProperties
            {
                Persistent = true
            };

            var messageBody = System.Text.Encoding.UTF8.GetBytes(body);

            await channel.BasicPublishAsync(exchange: string.Empty,
                          routingKey: queue, true,
                          basicProperties: props,
                          body: messageBody);
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
