using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TH.MongoRnDMS.App;

public partial class TreeFilterModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal Age { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string? GardenId { get; set; }
}