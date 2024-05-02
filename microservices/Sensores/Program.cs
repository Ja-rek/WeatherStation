using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensors;

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
                x.UsingRabbitMq((context,cfg) =>
                {
                    cfg.Host("my-release3-rabbitmq", "/", h => {
                        h.Username("user");
                        h.Password("SXghNIgaG1iV64oP");
                    });
                    
                    cfg.ConfigureEndpoints(context);
                });
            });
            
            services.AddScoped<IClassifierService, ClassifierService>();
            services.AddScoped<ISensor, SensorSimulation>();
            
            services.Configure<SensorConfig>(hostContext.Configuration.GetSection("Sensor"));
            services.AddHostedService<SensorBackgroundService>();
        });