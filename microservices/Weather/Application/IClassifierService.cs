using Contracts;

namespace Weather.Application;

public interface IClassifierService
{
    public string Classify(in TemperatureMeasuredEvent temperatureMeasured);
}