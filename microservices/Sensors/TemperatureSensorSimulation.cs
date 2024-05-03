using Contracts;
using Microsoft.Extensions.Options;
using Sensors.Config;

namespace Sensors;

public class TemperatureSensorSimulation(in IOptions<SensorConfig[]> sensorConfig) : ISensor
{
    private readonly SensorConfig sensor = sensorConfig.Value.First(x => x.Type == SensorType.Temperature);

    public async Task<TemperatureMeasuredEvent> MeasureAsync(CancellationToken cancellationToken = default)
    {
        var (_, minValue, maxValue, frequency) = sensor;
        var time = frequency * 1000;

        var random = new Random();
        var value = random.Next(minValue, maxValue);

        await Task.Delay(time, cancellationToken);

        return new TemperatureMeasuredEvent(value, maxValue, minValue, DateTimeOffset.Now);
    }
}
