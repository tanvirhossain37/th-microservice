using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TH.MongoRnDMS.App;

public partial class FruitFilterModel
{
    public string TreeName { get; set; }
    public int PageIndex { get; set; } = (int)PageEnum.PageIndex;
    public int PageSize { get; set; } = (int)PageEnum.PageSize;
}