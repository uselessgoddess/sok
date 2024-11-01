using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VRisc.GrpcServices;
using VRisc.UseCases.Interfaces;

namespace VRisc.Infrastructure.Broker;

public class CompileCheckConsumer(RabbitMQConnection mq, ICheckNotifier notifier) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken token)
    {
        var consumer = new EventingBasicConsumer(mq.Channel);
        consumer.Received += async (model, args) =>
        {
            var res = CheckResponse.Parser.ParseFrom(args.Body.ToArray());

            await notifier.Notify(res.User, res.Ok, res.Message);
        };
        mq.Channel.BasicConsume(queue: Queries.COMPILE_CHECK, autoAck: true, consumer);

        return Task.CompletedTask;
    }
}