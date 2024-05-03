using FluentNHibernate.Mapping;
using SensorHistoryApplication;

namespace SensoreHistoryInfrastructure;

public class MeasurementMap : ClassMap<Measurement>
{
    public MeasurementMap()
    {
        Id(x => x.Id);
        Map(x => x.Type);
        Map(x => x.Value);
        Map(x => x.Quality);
    }
}
