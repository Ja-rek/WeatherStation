using Contracts;

namespace Sensors;

public interface ISensor
{
    TemperatureMeasuredEvent Measure();
}