using Contracts;
using MassTransit;
using Moq;
using Weather.Application;
using Weather.Application.EventConsumers;
using Weather.Application.Temperature;

namespace Weather.UnitTests;

[TestFixture]
public class TemperatureMeasuredSaveConsumerTests
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
    public async Task Consume_ShouldSaveTemperatureMeasurementWithCorrectData()
    {
        // Arrange
        var mockMeasurementService = new Mock<IMeasurementService<TemperatureMeasurement>>();
        var consumer = new TemperatureMeasuredSaveConsumer(mockMeasurementService.Object, new TemperatureClassifier());

        // Act
        await consumer.Consume(stubConsumeContext.Object);

        // Assert
        mockMeasurementService.Verify(x => x.Save(It.Is<TemperatureMeasurement>(measurement =>
            measurement.Quality == "Alarm"
            && measurement.Value == temperatureMeasuredEvent.Value
            && measurement.Date == temperatureMeasuredEvent.Date
        )), Times.Once);
    }
}

