using MongoDB.Bson.Serialization.Attributes;
using TH.Common.Model;

namespace TH.ShadowMS.Core;

public class Shadow
{
    [BsonId] public string Id { get; set; } = null!;
    public string SpaceId { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; } = true;
    public string UserName { get; set; }
    public ActivityNameEnum ActivityName { get; set; }
    public string Message { get; set; }
}