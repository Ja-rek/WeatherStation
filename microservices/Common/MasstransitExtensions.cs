using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Common;

public static class MassTransitExtensions
{
    public static void AddHost(this IBusRegistrationConfigurator massTransitCfg, IConfiguration configuration) 
    {
        var host = configuration.GetSection("RabbitMq:Host").Value;
        var username = configuration.GetSection("RabbitMq:Username").Value;
        var password = configuration.GetSection("RabbitMq:Password").Value;

        massTransitCfg.UsingRabbitMq((context,cfg) =>
        {
            cfg.Host(host, "/", h => {
                h.Username(username);
                h.Password(password);
            });
            
            cfg.ConfigureEndpoints(context);
        });
    }
}
