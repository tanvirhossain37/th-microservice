using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TH.MongoRnDMS.App;

public partial class TreeFilterModel
{
    public string GardenName { get; set; }
    public int PageIndex { get; set; } = (int)PageEnum.PageIndex;
    public int PageSize { get; set; } = (int)PageEnum.PageSize;
}