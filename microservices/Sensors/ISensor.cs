using Contracts;

namespace Sensors;

public interface ISensor
{
    Task<TemperatureMeasuredEvent> Measure(CancellationToken cancellationToken = default);
}