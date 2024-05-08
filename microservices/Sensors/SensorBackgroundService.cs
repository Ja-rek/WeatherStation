using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sensors;

public class SensorBackgroundService(IBus bus, 
    ISensor sensor, 
    ILogger<SensorBackgroundService> logger) : BackgroundService
{
    private readonly IBus bus = bus;
    private readonly ISensor sensor = sensor;
    private readonly ILogger<SensorBackgroundService> logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var measurementEvent = await sensor.MeasureAsync();

            logger.LogInformation($"send message with value: {measurementEvent.Value}");

            await bus.Publish(measurementEvent, stoppingToken);
        }
    }
}