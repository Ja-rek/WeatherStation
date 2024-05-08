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
    x.AddConsumer<TemperatureMeasuredSaveConsumer>();
    x.AddConsumer<TemperatureMeasuredNotificationConsumer>();
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

services.AddSingleton(c => SessionFactoryProvider
    .CreateSessionFactory(m => m.FluentMappings.AddFromAssemblyOf<TemperatureMeasurementMap>(),
        builder.Configuration.GetSection("SQLConnectionString").Get<string>() ?? string.Empty));

services.AddSignalR();
services.AddScoped<INotification, Notification>();
services.AddScoped<IClassifierService, TemperatureClassifier>();
services.AddScoped<IMeasurementService<TemperatureMeasurement>,MeasurementService<TemperatureMeasurement>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins"); 
app.UseHttpsRedirection();

app.MapGet("/TemperatureMeasurement", async (IMeasurementService<TemperatureMeasurement> service) =>
   await service.Get(1000)
).WithOpenApi();

app.MapHub<NotificationHub>("/NotificationHub");

app.Run();
