namespace Sensors.Config;

public record SensorConfig
{
    public required SensorType Type { get; set;}
    public required int MinValue { get; set;}
    public required int MaxValue { get; set;}
    public string? EncoderType { get;}
    public required int Frequency 
    { 
        get => Frequency * 1000 ; 
        set => Frequency = value; 
    }
};