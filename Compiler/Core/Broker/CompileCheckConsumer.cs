using Compiler.Core.Interfaces;

namespace Compiler.Core.Broker;

using GrpcServices;
using Compiler.Data.Broker;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class CompileCheckConsumer(RabbitMQConnection mq, CompileCheckProducer prod, ICompileCheck check)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken token)
    {
        var consumer = new EventingBasicConsumer(mq.Channel);
        consumer.Received += async (model, args) =>
        {
            var req = CheckRequest.Parser.ParseFrom(args.Body.ToArray());

            var (ok, message) = await check.Check(req.Source.ToByteArray());

            prod.SendCheckResult(new CheckResponse
            {
                User = req.User,
                Ok = ok,
                Message = message
            });
        };
        mq.Channel.BasicConsume(queue: Queries.COMPILE_CHECK, autoAck: true, consumer);

        return Task.CompletedTask;
    }
}