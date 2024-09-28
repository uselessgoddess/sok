namespace VRisc.UseCases.Broker;

using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VRisc.Infrastructure.Broker;
using VRisc.Infrastructure.Services;
using VRisc.UseCases.Interfaces;

public class CompileCheckConsumer(RabbitMQConnection mq) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken token)
    {
        var consumer = new EventingBasicConsumer(mq.Channel);
        consumer.Received += (model, args) =>
        {
            var _result = Encoding.UTF8.GetString(args.Body.ToArray());
        };
        mq.Channel.BasicConsume(queue: "compile-check", autoAck: true, consumer);

        return Task.CompletedTask;
    }
}