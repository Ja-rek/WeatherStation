namespace Contracts;

public record TemperatureMeasuredNotificationEvent(int Value, string Quality, DateTimeOffset Date);
