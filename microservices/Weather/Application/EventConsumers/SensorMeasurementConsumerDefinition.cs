using MassTransit;

namespace Weather.Application.EventConsumers;

public class SensorMeasurementConsumerDefinition : ConsumerDefinition<TemperatureMeasuredConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<TemperatureMeasuredConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(5, 1000));
    }
}