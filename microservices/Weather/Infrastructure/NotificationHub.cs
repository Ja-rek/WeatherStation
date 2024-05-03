using Microsoft.AspNetCore.SignalR;

namespace Weather.Infrastructure;

public class NotificationHub : Hub
{
    public async Task Publish(string chanel, object message)
    {
        await Clients.All.SendAsync("Notification", chanel, message);
    }
}
