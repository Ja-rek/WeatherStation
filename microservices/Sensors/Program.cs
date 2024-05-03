using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensors;
using Sensors.Config;

await CreateHostBuilder(args).RunConsoleAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        })
        .ConfigureServices((hostContext, services) =>
        {
            var host = hostContext.Configuration.GetSection("RabbitMq:Host").Value;
            var username = hostContext.Configuration.GetSection("RabbitMq:Username").Value;
            var password = hostContext.Configuration.GetSection("RabbitMq:Password").Value;

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context,cfg) =>
                {
                    cfg.Host(host, "/", h => {
                        h.Username(username);
                        h.Password(password);
                    });
                    
                    cfg.ConfigureEndpoints(context);
                });
            });
            
            services.AddScoped<ISensor, TemperatureSensorSimulation>();
            services.Configure<SensorConfig[]>(hostContext.Configuration.GetSection("Sensors"));
            services.AddHostedService<SensorBackgroundService>();
        });