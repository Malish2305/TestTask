namespace TestTask.Domain.Exceptions;

public class SecureException : Exception
{
    public SecureException(string message) : base(message)
    {
        
    }

    public Guid EventId => Guid.NewGuid();
}