using Contracts;
using MassTransit;

namespace Weather.Application.EventConsumers;

public class TemperatureMeasuredNotificationConsumer(in INotification notification,
    in IClassifierService classifier) : IConsumer<TemperatureMeasuredEvent>
{
    private readonly INotification notification = notification;
    private readonly IClassifierService classifier = classifier;

    public async Task Consume(ConsumeContext<TemperatureMeasuredEvent> context)
    {
        var message = context.Message;
        
        var notificationEvent =  new TemperatureMeasuredNotificationEvent(message.Value, 
            classifier.Classify(message), 
            message.Date);

        await notification.PublishAsync("Temperature", notificationEvent);
    }
}