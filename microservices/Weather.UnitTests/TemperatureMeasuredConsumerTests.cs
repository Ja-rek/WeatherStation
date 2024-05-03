using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Weather.Application;
using Weather.Application.EventConsumers;
using Weather.Application.Temperature;

namespace Weather.UnitTests;

[TestFixture]
public class TemperatureMeasuredConsumerTests
{
    private TemperatureMeasuredEvent temperatureMeasuredEvent;
    private Mock<ILogger<TemperatureMeasuredConsumer>> mockLogger;
    private Mock<ConsumeContext<TemperatureMeasuredEvent>> mockConsumeContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        mockLogger = new Mock<ILogger<TemperatureMeasuredConsumer>>();

        temperatureMeasuredEvent = new TemperatureMeasuredEvent(10, 100, 0, DateTimeOffset.Now);
        mockConsumeContext = new Mock<ConsumeContext<TemperatureMeasuredEvent>>();
        mockConsumeContext.Setup(x => x.Message).Returns(temperatureMeasuredEvent);
    }

    [Test]
    public async Task Consume_ShouldSaveTemperatureMeasurementWithCorrectData()
    {
        // Arrange
        var mockMeasurementService = new Mock<IMeasurementService<TemperatureMeasurement>>();
        var consumer = new TemperatureMeasuredConsumer(mockLogger.Object, mockMeasurementService.Object, new TemperatureClassifier());

        // Act
        await consumer.Consume(mockConsumeContext.Object);

        // Assert
        mockMeasurementService.Verify(x => x.Save(It.Is<TemperatureMeasurement>(measurement =>
            measurement.Quality == "Alarm"
            && measurement.Value == temperatureMeasuredEvent.Value
            && measurement.Date == temperatureMeasuredEvent.Date
        )), Times.Once);
    }
}

