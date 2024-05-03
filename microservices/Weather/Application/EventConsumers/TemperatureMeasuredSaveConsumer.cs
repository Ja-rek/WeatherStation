using Contracts;
using MassTransit;
using Weather.Application.Temperature;

namespace Weather.Application.EventConsumers;

public class TemperatureMeasuredSaveConsumer(in IMeasurementService<TemperatureMeasurement> service,
    in IClassifierService classifier) : IConsumer<TemperatureMeasuredEvent>
{
    private readonly IMeasurementService<TemperatureMeasurement> service = service;
    private readonly IClassifierService classifier = classifier;

    public async Task Consume(ConsumeContext<TemperatureMeasuredEvent> context)
    {
        var message = context.Message;

        await service.Save(new TemperatureMeasurement 
        { 
            Quality = classifier.Classify(message), 
            Value = message.Value,
            Date = message.Date,
        });
    }
}