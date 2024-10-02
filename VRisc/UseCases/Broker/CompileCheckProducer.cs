﻿namespace VRisc.UseCases.Broker;

using Google.Protobuf;
using RabbitMQ.Client;
using VRisc.GrpcServices;
using VRisc.Infrastructure.Broker;

public class CompileCheckProducer(RabbitMQConnection mq)
{
    public void SendPotentialAsm(string user, byte[] bytes)
    {
        var props = mq.Channel.CreateBasicProperties();
        var body = new CheckRequest
        {
            User = user, Source = ByteString.CopyFrom(bytes),
        };

        mq.Channel.BasicPublish(
            exchange: string.Empty,
            routingKey: Queries.COMPILE_CHECK,
            basicProperties: props,
            body: body.ToByteArray());
    }
}