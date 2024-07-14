namespace TH.EventBus.Messages;

public abstract class BaseEvent
{
    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; } = true;
}