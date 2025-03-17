namespace Lab1.Core.Exceptions;

public class InvalidProductException : Exception
{
    public InvalidProductException(string message) : base(message) { }
}
