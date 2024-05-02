﻿using Contracts;
using Microsoft.Extensions.Options;
using Sensors.Config;

namespace Sensors;

public class TemperatureSensorSimulation(in IOptions<SensorConfig[]> sensorConfig) : ISensor
{
    private readonly int maxValue = sensorConfig.Value.First(x => x.Type == SensorType.Temperature).MaxValue;
    private readonly int minValue = sensorConfig.Value.First(x => x.Type == SensorType.Temperature).MinValue;
    private readonly int time = sensorConfig.Value.First(x => x.Type == SensorType.Temperature).Frequency * 1000;

    public TemperatureMeasuredEvent Measure()
    {
        var random = new Random();
        var value = random.Next(minValue, maxValue);

        Thread.Sleep(time);

        return new TemperatureMeasuredEvent(value, maxValue, minValue, DateTimeOffset.Now);
    }
}