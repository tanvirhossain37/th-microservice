using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TH.MongoRnDMS.App;

public partial class GardenFilterModel
{
    public int PageIndex { get; set; } = (int)PageEnum.PageIndex;
    public int PageSize { get; set; } = (int)PageEnum.PageSize;
}