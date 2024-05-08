using Contracts;
using Sensors.Config;

namespace Sensors;

public class TemperatureSensorSimulation(SensorConfig[] sensorConfig) : ISensor
{
    private readonly SensorConfig[] sensorConfig = sensorConfig;

    public async Task<TemperatureMeasuredEvent> MeasureAsync(CancellationToken cancellationToken = default)
    {
        var (_, minValue, maxValue, frequency) = sensorConfig.First(x => x.Type == SensorType.Temperature);
        var time = frequency * 1000;

        var random = new Random();
        var value = random.Next(minValue, maxValue);

        await Task.Delay(time, cancellationToken);

        return new TemperatureMeasuredEvent(value, maxValue, minValue, DateTimeOffset.Now);
    }
}
