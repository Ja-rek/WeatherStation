using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sensors;

public class SensorBackgroundService(IBus bus, ISensor sensor, ILogger<SensorBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var measurementEvent = sensor.Measure();

            logger.LogInformation($"send message with value: {measurementEvent.Value}");

            await bus.Publish(measurementEvent);
        }
    }
}