using System;
using System.Collections.Generic;

namespace TH.UserSvc.Core;

public partial class UserPhone : BaseEntity
{
    public string Number { get; set; } = null!;

    public bool IsDefault { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}