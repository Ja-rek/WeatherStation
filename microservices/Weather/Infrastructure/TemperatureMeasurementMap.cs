using FluentNHibernate.Mapping;
using Weather.Application.Temperature;

namespace Weather.Infrastructure;

public class TemperatureMeasurementMap : ClassMap<TemperatureMeasurement>
{
    public TemperatureMeasurementMap()
    {
        Table("Sensors");
        Id(x => x.Id);
        Map(x => x.Value);
        Map(x => x.Quality);
        Map(x => x.Date);
    }
}
