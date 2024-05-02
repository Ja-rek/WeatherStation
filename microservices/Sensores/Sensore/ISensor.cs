using Contracts;

namespace Sensors;

public interface ISensor
{
    SensorMeasurementEvent Measure();
}