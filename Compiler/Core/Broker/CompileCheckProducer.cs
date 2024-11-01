using Compiler.GrpcServices;
using Google.Protobuf;

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
            routingKey: Queries.COMPILE_CHECK,
            basicProperties: props,
            body: res.ToByteArray());
    }
}