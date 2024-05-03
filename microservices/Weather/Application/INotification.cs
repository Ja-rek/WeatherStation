namespace Weather.Application;

public interface INotification
{
    Task PublishAsync(string chanel, object message);
}
