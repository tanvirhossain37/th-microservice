using System;
using System.Collections.Generic;

namespace TH.RnD.CL.Models;

public partial class Branch
{
    public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string SpaceId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDefault { get; set; }

    public virtual ICollection<BranchUser> BranchUsers { get; set; } = new List<BranchUser>();

    public virtual Company Company { get; set; } = null!;
}
