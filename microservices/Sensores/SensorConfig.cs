namespace Sensors;

public record SensorConfig(int Id, string Type, int MinValue, int MaxValue, string EncoderType, int Frequency);