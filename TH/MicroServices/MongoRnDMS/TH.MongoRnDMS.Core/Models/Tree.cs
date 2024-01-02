using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TH.MongoRnDMS.Core;

public partial class Tree
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Age { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string? GardenId { get; set; }

    public virtual ICollection<Fruit> Fruits { get; set; } = new List<Fruit>();

    public virtual Garden? Garden { get; set; }
}
