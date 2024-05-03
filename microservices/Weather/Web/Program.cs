using Common;
using MassTransit;
using Weather.Application;
using Weather.Application.EventConsumers;
using Weather.Application.Temperature;
using Weather.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddMassTransit(x =>
{
    x.AddConsumer<TemperatureMeasuredConsumer, TemperatureMeasuredConsumerDefinition>();
    x.AddHost(builder.Configuration);
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

services.AddScoped<IMeasurementService<TemperatureMeasurement>,MeasurementService<TemperatureMeasurement>>();

services.AddSingleton(c => SessionFactoryProvider
    .CreateSessionFactory(m => m.FluentMappings.AddFromAssemblyOf<TemperatureMeasurement>(),
    "Server=mssql-service,1433;Database=Sensor;User Id=sa;Password=Password123!;"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins"); 

app.UseHttpsRedirection();

app.MapGet("/TemperatureMeasurement", async (IMeasurementService<TemperatureMeasurement> service) =>
{
    return await service.Get(1000);
}).WithOpenApi();

app.Run();
