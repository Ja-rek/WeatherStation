using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Weather.Infrastructure;

public class SessionFactoryProvider
{
    public static ISessionFactory CreateSessionFactory(Action<MappingConfiguration> mappingConfiguration, string sqlString)
    {
        var createDb = false;

        Action<NHibernate.Cfg.Configuration> schemaConfig = (cfg) =>
        {
            cfg.Properties["show_sql"] = "true";
            cfg.Properties["prepare_sql"] = "true";

            var update = new SchemaUpdate(cfg);
            update.Execute(false, true);

            if (createDb) new SchemaExport(cfg).Create(true, createDb);
        };

        var factory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(sqlString).ShowSql())
            .Mappings(mappingConfiguration)
            .ExposeConfiguration(schemaConfig)
            .BuildSessionFactory();

        return factory;
    }
}
