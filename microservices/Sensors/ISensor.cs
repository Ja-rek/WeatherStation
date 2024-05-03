using Contracts;

namespace Sensors;

public interface ISensor
{
    Task<TemperatureMeasuredEvent> MeasureAsync(CancellationToken cancellationToken = default);
}