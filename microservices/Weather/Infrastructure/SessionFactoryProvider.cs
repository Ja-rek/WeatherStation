using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace SensoreHistoryInfrastructure;

public class SessionFactoryProvider
{
    public static ISessionFactory CreateSessionFactory(Action<MappingConfiguration> mappingConfiguration)
    {
        var sqlString = "Server=mssql-service,1433;Database=Sensor;User Id=sa;Password=Password123!;";

        var createDb = false;

        Action<NHibernate.Cfg.Configuration> shemaConfig = (cfg) =>
        {
            cfg.Properties["show_sql"] = "true";
            cfg.Properties["prepare_sql"] = "true";

            var update = new SchemaUpdate(cfg);
            update.Execute(false, true);

            if (createDb) new SchemaExport(cfg).Create(true, createDb);
        };

        var factory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql7.ConnectionString(sqlString).ShowSql())
            .Mappings(mappingConfiguration)
            .ExposeConfiguration(shemaConfig)
            .BuildSessionFactory();

        return factory;
    }
}
