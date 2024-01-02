using System;
using System.Collections.Generic;

namespace TH.RnD.API.TreeModels;

public partial class Fruit
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Age { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string TreeId { get; set; } = null!;

    public virtual Tree Tree { get; set; } = null!;
}
