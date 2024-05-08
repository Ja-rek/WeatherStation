using Microsoft.AspNetCore.SignalR;
using Weather.Application;

namespace Weather.Infrastructure;

public class Notification(IHubContext<NotificationHub> hubContext) : INotification
{
    private readonly IHubContext<NotificationHub> hubContext = hubContext;

    public async Task PublishAsync(string chanel, object message)
    {
        await hubContext.Clients.All.SendAsync(chanel, message);
    }
}
