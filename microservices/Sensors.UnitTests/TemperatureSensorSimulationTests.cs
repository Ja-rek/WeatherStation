using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Sensors.Config;

namespace Sensors.UnitTests;

[TestFixture]
public class TemperatureSensorSimulationTests
{
    private Mock<IOptions<SensorConfig[]>> sensorConfigMock;

    [SetUp]
    public void SetUpOne()
    {
        sensorConfigMock = new Mock<IOptions<SensorConfig[]>>();

        SensorConfig[] config = [
            new SensorConfig
            {
                Type = SensorType.Temperature,
                MinValue = 0,
                MaxValue = 100,
                Frequency = 1
            }
        ];

        sensorConfigMock.Setup(x => x.Value).Returns(config);
    }

    [Test]
    public async Task MeasureAsync_ShouldReturnSensorMeasurementEventWithCorrectValues()
    {
        // Arrange
        var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2)); // Stop after 2 seconds
        var sensorSimulation = new TemperatureSensorSimulation(sensorConfigMock.Object);

        // Act
        var measurement = await sensorSimulation.MeasureAsync(cancellationTokenSource.Token);

        // Assert
        measurement.Should().NotBeNull();
        measurement.Value.Should().BeInRange(0, 100); // Assuming the value is within the specified range
    }
}

