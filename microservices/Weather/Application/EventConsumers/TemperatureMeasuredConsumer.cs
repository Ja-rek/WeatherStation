using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Weather.Application.Temperature;

namespace Weather.Application.EventConsumers;

public class TemperatureMeasuredConsumer(in ILogger<TemperatureMeasuredConsumer> logger, 
    in IMeasurementService<TemperatureMeasurement> service,
    in IClassifierService classifier) : IConsumer<TemperatureMeasuredEvent>
{
    private readonly ILogger<TemperatureMeasuredConsumer> logger = logger;
    private readonly IMeasurementService<TemperatureMeasurement> service = service;
    private readonly IClassifierService classifier = classifier;

    public async Task Consume(ConsumeContext<TemperatureMeasuredEvent> context)
    {
        var message = context.Message;

        logger.LogInformation($"Consume {nameof(TemperatureMeasuredEvent)}");

        await service.Save(new TemperatureMeasurement 
        { 
            Quality = classifier.Classify(message), 
            Value = message.Value,
            Date = message.Date,
        });
    }
}