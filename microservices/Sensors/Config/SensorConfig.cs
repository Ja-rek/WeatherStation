namespace Sensors.Config;

public record SensorConfig
{
    private int frequency;

    public required SensorType Type { get; set;}
    public required int MinValue { get; set;}
    public required int MaxValue { get; set;}
    public string? EncoderType { get;}
    public required int Frequency 
    { 
        get => frequency * 1000 ; 
        set => frequency = value; 
    }
};