using TH.Common.Core;

namespace TH.EventBus.Messages;

public class ShadowEvent : BaseEvent
{
    public string SpaceId { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; }
    public string UserName { get; set; }
    public ActivityNameEnum ActivityName { get; set; }

    public string Message { get; set; }
}