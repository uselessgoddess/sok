using Google.Protobuf;
using GrpcServices;

namespace Compiler.Core.Broker;

using Compiler.Data.Broker;
using RabbitMQ.Client;

public class CompileCheckProducer(RabbitMQConnection mq)
{
    public void SendCheckResult(CheckResponse res)
    {
        var props = mq.Channel.CreateBasicProperties();

        mq.Channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "compile-check",
            basicProperties: props,
            body: res.ToByteArray());
    }
}