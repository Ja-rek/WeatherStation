using MassTransit;
using SensorHistoryWeb.Consumers;

namespace Notifications.Consumers;

public class SensorMeasurementConsumerConsumerDefinition : ConsumerDefinition<SensorMeasurementConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<SensorMeasurementConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(5, 1000));
    }
}