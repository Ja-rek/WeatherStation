using Contracts;
using Microsoft.Extensions.Options;
using Sensors.Config;

namespace Sensors;

public class TemperatureSensorSimulation(in IOptions<SensorConfig[]> sensorConfig) : ISensor
{
    private readonly int maxValue = sensorConfig.Value.First(x => x.Type == SensorType.Temperature).MaxValue;
    private readonly int minValue = sensorConfig.Value.First(x => x.Type == SensorType.Temperature).MinValue;
    private readonly int time = sensorConfig.Value.First(x => x.Type == SensorType.Temperature).Frequency;

    public async Task<TemperatureMeasuredEvent> MeasureAsync(CancellationToken cancellationToken = default)
    {
        var random = new Random();
        var value = random.Next(minValue, maxValue);

        await Task.Delay(time, cancellationToken);

        return new TemperatureMeasuredEvent(value, maxValue, minValue, DateTimeOffset.Now);
    }
}
