using NHibernate;
using NHibernate.Linq;
using Weather.Application;

namespace Weather.Infrastructure;

public class MeasurementService<T>(ISessionFactory sessionFactory) : IMeasurementService<T> where T : class
{
    private readonly ISessionFactory sessionFactory = sessionFactory;

    public async Task<IEnumerable<T>> Get(int range)
    {
        using (var session = sessionFactory.OpenSession())
        {
            return await session.Query<T>()
                .FetchLazyProperties()
                .Take(range)
                .ToListAsync();
        }
    }

    public async Task Save(T measurement)
    {
        using (var session = sessionFactory.OpenSession())
        using(var transaction = session.BeginTransaction())
        {
            await session.SaveAsync(measurement);
            await transaction.CommitAsync();
        }
    }
}