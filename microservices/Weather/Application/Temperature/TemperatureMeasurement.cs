namespace Weather.Application.Temperature;

public class TemperatureMeasurement
{
    public virtual int Id { get; init; }
    public required virtual int Value { get; set; }
    public required virtual string Quality { get; set; }
    public required virtual DateTimeOffset Date { get; set; }
}
