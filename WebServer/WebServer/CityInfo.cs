using System;
using System.Collections.Generic;

namespace WebServer;

public partial class CityInfo
{
    public long Id { get; set; }

    public double[] MaxWeather { get; set; } = null!;

    public double[] MinWeather { get; set; } = null!;

    public double[] PeriodWeather { get; set; } = null!;

    public double CurrentWeather { get; set; }

    public long CityId { get; set; }

    public virtual City City { get; set; } = null!;
}
