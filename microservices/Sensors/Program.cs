using Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
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
            services.AddMassTransit(x =>
            {
                x.AddHost(hostContext.Configuration);
            });
            
            services.AddScoped<ISensor, TemperatureSensorSimulation>();
            services.AddSingleton(hostContext.Configuration.GetSection("Sensors").Get<SensorConfig[]>() ?? []);

            services.AddHostedService<SensorBackgroundService>();
        });