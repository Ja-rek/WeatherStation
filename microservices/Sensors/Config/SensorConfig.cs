namespace Sensors.Config;

public record SensorConfig
{
    public int Id { get; }
    public SensorType Type { get; }
    public int MinValue { get; }
    public int MaxValue { get; }
    public string? EncoderType { get; }
    public int Frequency { get => Frequency * 1000 ; }
};