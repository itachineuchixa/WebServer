using System;
using System.Collections.Generic;

namespace WebServer;

public partial class City
{
    public long Id { get; set; }

    public string City1 { get; set; } = null!;

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public virtual ICollection<CityInfo> CityInfos { get; } = new List<CityInfo>();

    public virtual ICollection<UserInfo> UserInfos { get; } = new List<UserInfo>();
}
