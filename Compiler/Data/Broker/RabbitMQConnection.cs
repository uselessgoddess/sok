using RabbitMQ.Client;

namespace Compiler.Data.Broker;

public class RabbitMQConnection : IDisposable
{
    private readonly IConnection connection;
    private IModel _channel;

    public IModel Channel
    {
        get
        {
            if (_channel.IsClosed)
            {
                _channel = connection.CreateModel();
            }

            return _channel;
        }
        private init => _channel = value;
    }

    public RabbitMQConnection(string host)
    {
        var factory = new ConnectionFactory { HostName = host };
        connection = factory.CreateConnection();
        Channel = connection.CreateModel();
    }

    public void Dispose()
    {
        Channel.Close();
        connection.Close();
    }
}