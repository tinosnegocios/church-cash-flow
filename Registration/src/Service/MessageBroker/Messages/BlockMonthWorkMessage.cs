﻿using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace MessageBroker.Messages;

public class BlockMonthWorkMessage : BaseMessageBrokerClient
{
    public BlockMonthWorkMessage(IConfiguration configuration, int churchId, string competence) : base(configuration)
    {
        ChurchId = churchId;
        YearMonth = competence;

        LoadConfig();
    }

    public int ChurchId { get; private set; }
    public string YearMonth { get; private set; }

    protected override void LoadConfig()
    {
        Exchange = _configuration["MonthWorkMessageBroker:Exchange"]!;
        Host = _configuration["MonthWorkMessageBroker:Host"]!;
        VirtualHost = _configuration["MonthWorkMessageBroker:VirtualHost"]!;
        Port = _configuration["MonthWorkMessageBroker:Port"]!;
        UserName = _configuration["MonthWorkMessageBroker:UserName"]!;
        Password = _configuration["MonthWorkMessageBroker:Password"]!;
        RoutingKey = _configuration["MonthWorkMessageBroker:RoutingKey"]!;
        Queue = _configuration["MonthWorkMessageBroker:Queue"]!;
        ExchangeDeadLeatter = $"{Exchange}_dead_leatter";
        RoutingKeyDeadLeatter = $"{RoutingKey}_dead_leatter";
        QueueDeadLeatter = $"{Queue}_dead_leatter";

        BodyMessage = BuildMessage();
    }

    public void PreparePublish()
    {
        IMessageBrokerClient rabbitClient = new RabbitMqClient<BlockMonthWorkMessage>(this);

        rabbitClient.Publish();
    }

    protected override byte[] BuildMessage()
    {
        var objBody = new
        {
            ChurchId,
            YearMonth
        };

        var serialize = JsonSerializer.Serialize(objBody);
        var body = Encoding.UTF8.GetBytes(serialize);

        return body;
    }
}