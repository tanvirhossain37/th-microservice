using TH.Common.Model;

namespace TH.EventBus.Messages;

public class ShadowEvent : BaseEvent
{
    public string SpaceId { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string UserName { get; set; }
    public ActivityNameEnum ActivityName { get; set; }

    public string Message { get; set; }
}