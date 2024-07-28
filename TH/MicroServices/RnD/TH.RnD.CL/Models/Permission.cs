using System;
using System.Collections.Generic;

namespace TH.RnD.CL.Models;

public partial class Permission
{
    public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string SpaceId { get; set; } = null!;

    public string CompanyId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public bool Read { get; set; }

    public bool Write { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }

    public int AccessTypeId { get; set; }

    public string? ParentId { get; set; }

    public int MenuOrder { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Permission> InverseParent { get; set; } = new List<Permission>();

    public virtual Module Module { get; set; } = null!;

    public virtual Permission? Parent { get; set; }

    public virtual Role Role { get; set; } = null!;
}
