using System;
using System.Collections.Generic;

namespace WebServer;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<UserInfo> UserInfos { get; } = new List<UserInfo>();
}
