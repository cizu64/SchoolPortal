using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MESSAGEBUS.RabbitMQ;

public class ConnectionManager : IDisposable
{
     private static IConnection _connection;

    public static async Task<IConnection> CreateConnection(string host, int port, string username, string password)
    {

        if (_connection is null || !_connection.IsOpen)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = host,
                Port = port,
                UserName = username,
                Password = password
            };
            _connection = await connectionFactory.CreateConnectionAsync();
        }
        return _connection;
    }
    public async void Dispose()
    {
        await _connection.DisposeAsync();
    }

}