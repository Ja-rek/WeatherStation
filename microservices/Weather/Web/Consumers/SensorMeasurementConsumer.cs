using Contracts;
using MassTransit;

namespace SensorHistoryWeb.Consumers;

public class SensorMeasurementConsumer(ILogger<SensorMeasurementConsumer> logger) : IConsumer<SensorMeasurementEvent>
{
    public async Task Consume(ConsumeContext<SensorMeasurementEvent> context)
    {
        var message = context.Message;

        logger.LogInformation("Consume message");

        //await service.SaveMeasurements(new Measurement 
        //{ 
        //    Id = message.Id, 
        //    Quality = message.Quality, 
        //    Type = message.Type, 
        //    Value = message.Value 
        //});
    }
}