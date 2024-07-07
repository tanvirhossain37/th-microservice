namespace TH.EventBus.Messages;

public class EmailEvent : BaseEvent
{
    public List<string> To { get; set; } = new List<string>();
    public List<string> Cc { get; set; } = new List<string>();
    public List<string> Bcc { get; set; } = new List<string>();
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}