using System;
using System.Collections.Generic;

namespace TH.RnD.CL.Models;

public partial class BranchUser
{
    public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string SpaceId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string BranchId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public bool IsDefault { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
