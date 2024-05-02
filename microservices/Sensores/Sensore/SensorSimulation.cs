using Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sensors;

public class SensorSimulation(IOptions<SensorConfig> sensorConfig, IClassifierService classifierService, ILogger<SensorBackgroundService> logger) : ISensor
{
    public SensorMeasurementEvent Measure()
    {
        var random = new Random();
        var value = random.Next(sensorConfig.Value.MinValue, sensorConfig.Value.MaxValue);
        var time = sensorConfig.Value.Frequency * 1000;

        var quality = classifierService.Classify(value);

        logger.LogInformation($"config minValue: {sensorConfig.Value.MinValue}");
        logger.LogInformation($"config maxValue: {sensorConfig.Value.MaxValue}");

        Thread.Sleep(time);

        return new SensorMeasurementEvent { Type = "Speed", Value = value, Quality = quality };
    }
}
