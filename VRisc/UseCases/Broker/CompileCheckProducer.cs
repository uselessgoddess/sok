namespace VRisc.UseCases.Broker;

using RabbitMQ.Client;
using VRisc.Infrastructure.Broker;
using VRisc.Infrastructure.Services;

public class CompileCheckProducer(RabbitMQConnection mq)
{
    public void SendPotentialAsm(byte[] bytes)
    {
        var props = mq.Channel.CreateBasicProperties();

        mq.Channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "compile-check",
            basicProperties: props,
            body: bytes);
    }
}