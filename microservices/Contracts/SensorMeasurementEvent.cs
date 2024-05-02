namespace Contracts;

public class SensorMeasurementEvent
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Value { get; set; }
    public string Quality { get; set; }
}
