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
            var bytes = CheckRequest.Parser.ParseFrom(args.Body.ToArray()).Source;
            
            var (ok, message) = await check.Check(bytes.ToByteArray());
            
            prod.SendCheckResult(new CheckResponse
            {
                Ok = ok,
                Message = message
            });
        };
        mq.Channel.BasicConsume(queue: "compile-check", autoAck: true, consumer);

        return Task.CompletedTask;
    }
}