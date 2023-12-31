using System;
using System.Collections.Generic;

namespace TH.RnD.CL.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Surname { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<UserEmail> UserEmails { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserPhone> UserPhones { get; set; } = new List<UserPhone>();
}
