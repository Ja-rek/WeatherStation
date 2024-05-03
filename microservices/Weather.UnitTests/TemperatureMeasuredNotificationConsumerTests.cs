using Contracts;
using MassTransit;
using Moq;
using Weather.Application;
using Weather.Application.EventConsumers;
using Weather.Application.Temperature;

namespace Weather.UnitTests;

[TestFixture]
public class TemperatureMeasuredNotificationConsumerTests
{
    private TemperatureMeasuredEvent temperatureMeasuredEvent;
    private Mock<ConsumeContext<TemperatureMeasuredEvent>> stubConsumeContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        temperatureMeasuredEvent = new TemperatureMeasuredEvent(10, 100, 0, DateTimeOffset.Now);
        stubConsumeContext = new Mock<ConsumeContext<TemperatureMeasuredEvent>>();
        stubConsumeContext.Setup(x => x.Message).Returns(temperatureMeasuredEvent);
    }

    [Test]
    public async Task Consume_ShouldNotifyWithCorrectData()
    {
        // Arrange
        var mockNotification = new Mock<INotification>();
        var consumer = new TemperatureMeasuredNotificationConsumer(mockNotification.Object, new TemperatureClassifier());

        // Act
        await consumer.Consume(stubConsumeContext.Object);

        // Assert
        mockNotification.Verify(x => x.PublishAsync(It.IsAny<string>(), It.Is<TemperatureMeasuredNotificationEvent>(measurement =>
            measurement.Quality == "Alarm"
            && measurement.Value == temperatureMeasuredEvent.Value
            && measurement.Date == temperatureMeasuredEvent.Date
        )), Times.Once);
    }
}

