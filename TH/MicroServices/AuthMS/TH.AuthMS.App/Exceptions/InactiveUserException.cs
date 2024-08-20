namespace TH.AuthMS.App;

public class InactiveUserException : Exception
{
    public InactiveUserException(string message) : base(message)
    {
    }
}