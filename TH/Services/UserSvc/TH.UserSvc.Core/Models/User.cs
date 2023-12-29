using System;
using System.Collections.Generic;

namespace TH.UserSvc.Core;

public partial class User : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string? Surname { get; set; }

    public virtual ICollection<UserEmail> UserEmails { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserPhone> UserPhones { get; set; } = new List<UserPhone>();
}