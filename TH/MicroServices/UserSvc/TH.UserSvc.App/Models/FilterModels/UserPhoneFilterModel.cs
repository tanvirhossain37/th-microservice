using System;
using System.Collections.Generic;

namespace TH.UserSvc.App;

public class UserPhoneFilterModel
{
    public string Id { get; set; } = null!;

    public string Number { get; set; } = null!;

    public bool IsDefault { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }
}