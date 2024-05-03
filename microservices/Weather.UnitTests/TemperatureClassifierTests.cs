using Contracts;
using FluentAssertions;
using Weather.Application.Temperature;

namespace Weather.UnitTests;

[TestFixture]
public class TemperatureClassifierTests
{
    private TemperatureMeasuredEvent temperatureMeasured;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        temperatureMeasured = new TemperatureMeasuredEvent(0, 100, 0, DateTimeOffset.Now);
    }

    [Test]
    public void Classify_ShouldReturnAlarm_WhenValueIsBelowTenPercentOfRange()
    {
        // Arrange
        var classifierService = new TemperatureClassifier();

        // Act
        var result = classifierService.Classify(temperatureMeasured with { Value = -5 });

        // Assert
        result.Should().Be("Alarm");
    }

    [Test]
    public void Classify_ShouldReturnAlarm_WhenValueIsAboveNinetyPercentOfRange()
    {
        // Arrange
        var classifierService = new TemperatureClassifier();

        // Act
        var result = classifierService.Classify(temperatureMeasured with { Value = 105 });

        // Assert
        result.Should().Be("Alarm");
    }

    [Test]
    public void Classify_ShouldReturnWarning_WhenValueIsBetweenTenAndTwentyFivePercentOfRange()
    {
        // Arrange
        var classifierService = new TemperatureClassifier();

        // Act
        var result = classifierService.Classify(temperatureMeasured with { Value = 15 });

        // Assert
        result.Should().Be("Warning");
    }

    [Test]
    public void Classify_ShouldReturnWarning_WhenValueIsBetweenSeventyFiveAndNinetyPercentOfRange()
    {
        // Arrange
        var classifierService = new TemperatureClassifier();

        // Act
        var result = classifierService.Classify(temperatureMeasured with { Value = 85 });

        // Assert
        result.Should().Be("Warning");
    }

    [Test]
    public void Classify_ShouldReturnNormal_WhenValueIsBetweenTwentyFiveAndSeventyFivePercentOfRange()
    {
        // Arrange
        var classifierService = new TemperatureClassifier();

        // Act
        var result = classifierService.Classify(temperatureMeasured with { Value = 50 });

        // Assert
        result.Should().Be("Normal");
    }
}

