namespace Contracts;

public record TemperatureMeasuredEvent(int Value, int MaxValue, int MinValue, DateTimeOffset Date);
