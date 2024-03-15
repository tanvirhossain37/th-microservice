namespace TH.EventBus.Messages;

public abstract class BaseEvent
{
    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public BaseEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }
}