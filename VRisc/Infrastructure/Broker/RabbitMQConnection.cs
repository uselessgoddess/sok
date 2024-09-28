namespace VRisc.Infrastructure.Broker;

using RabbitMQ.Client;

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

        Channel.QueueDeclare(Queries.COMPILE_CHECK, exclusive: false);
    }

    public void Dispose()
    {
        Channel.Close();
        connection.Close();
    }
}