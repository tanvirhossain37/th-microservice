using System;
using System.Collections.Generic;

namespace TH.UserSvc.App;

public class UserFilterModel
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Surname { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }
}