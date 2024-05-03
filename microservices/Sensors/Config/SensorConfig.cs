namespace Sensors.Config;

public record SensorConfig(SensorType Type, int MinValue, int MaxValue, int Frequency)
{
    public SensorConfig() : this(default, default, default, default)
    {
    }
}
