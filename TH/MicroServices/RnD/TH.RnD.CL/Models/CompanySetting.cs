using System;
using System.Collections.Generic;

namespace TH.RnD.CL.Models;

public partial class CompanySetting
{
    public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string SpaceId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public bool Value { get; set; }

    public int ModuleId { get; set; }

    public virtual Company Company { get; set; } = null!;
}
