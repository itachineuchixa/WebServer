using System;
using System.Collections.Generic;

namespace WebServer;

public partial class UserInfo
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long CityId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual City City { get; set; } = null!;
}
