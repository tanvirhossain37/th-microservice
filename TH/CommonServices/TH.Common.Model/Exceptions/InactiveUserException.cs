namespace TH.Common.Model;

public class InactiveUserException : Exception
{
    public InactiveUserException(string message) : base(message)
    {
    }
}