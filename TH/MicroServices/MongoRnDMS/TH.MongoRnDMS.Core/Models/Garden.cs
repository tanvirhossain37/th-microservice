using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TH.MongoRnDMS.Core;

public partial class Garden
{
    public string Name { get; set; } = null!;

    public string? Owner { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Tree> Trees { get; set; } = new List<Tree>();
}
