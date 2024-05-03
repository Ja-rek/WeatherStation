namespace Weather.Application;

public interface IMeasurementService<T> where T : class
{
    Task<IEnumerable<T>> Get(int range);
    Task Save(T measurement);
}