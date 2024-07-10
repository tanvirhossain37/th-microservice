using MongoDB.Bson.Serialization.Attributes;
using TH.ShadowMS.Core.Enums;

namespace TH.ShadowMS.Core.Models;

public class Shadow
{
    [BsonId] public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }
    public string UserName { get; set; }
    public ActivityNameEnum ActivityNameEnum { get; set; }
    public string Message { get; set; }

}