using Contracts;

namespace Weather.Application.Temperature;

public class TemperatureClassifier : IClassifierService
{
    public string Classify(in TemperatureMeasuredEvent temperatureMeasured)
    {
        var (value, maxValue, minValue, _) = temperatureMeasured;

        var range = maxValue - minValue;

        var tenPercentOfRange = 0.1 * range;
        var twentyFivePercent = 0.25 * range;
        var seventyFivePercent = 0.75 * range;
        var ninetyPercent = 0.9 * range;

        return value switch
        {
            var v when v <= tenPercentOfRange || v >= ninetyPercent => "Alarm",
            var v when (v >= tenPercentOfRange && v <= twentyFivePercent)
                       || (v >= seventyFivePercent && v <= ninetyPercent) => "Warning",
            var v when v >= twentyFivePercent && v <= seventyFivePercent => "Normal",
            _ => throw new ApplicationException($"Method have to return result.")
        };
    }
}
