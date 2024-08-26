namespace TH.EventBus.Messages;

public class UserCreateEvent : BaseEvent
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string ReferralId { get; set; }
    public string CompanyName { get; set; }
    public bool IsAutoUserName { get; set; }
}