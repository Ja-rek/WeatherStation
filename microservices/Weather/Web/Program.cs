using MassTransit;
using Notifications.Consumers;
using SensoreHistoryInfrastructure;
using SensorHistoryApplication;
using SensorHistoryWeb.Consumers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddMassTransit(x =>
{
    x.AddConsumer<SensorMeasurementConsumer, SensorMeasurementConsumerConsumerDefinition>();
    x.UsingRabbitMq((context,cfg) =>
    {
        cfg.Host("my-release3-rabbitmq", "/", h => {
            h.Username("user");
            h.Password("SXghNIgaG1iV64oP");
        });
        
        cfg.ConfigureEndpoints(context);
    });
});

services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        });
});


services.AddScoped<MeasurementService>();
services.AddSingleton(c => SessionFactoryProvider
    .CreateSessionFactory(m => m.FluentMappings.AddFromAssemblyOf<MeasurementMap>()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins"); 

app.UseHttpsRedirection();

app.MapGet("/Mesurments", async (MeasurementService service) =>
{
    return await service.GetMeasurements(1000);
}).WithOpenApi();

app.Run();
