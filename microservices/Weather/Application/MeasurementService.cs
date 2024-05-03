using NHibernate;
using NHibernate.Linq;

namespace Weather.Application;

public class MeasurementService<T>(in ISessionFactory sessionFactory) : IMeasurementService<T> where T : class
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