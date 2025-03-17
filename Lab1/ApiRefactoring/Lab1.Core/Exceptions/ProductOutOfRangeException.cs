namespace Lab1.Core.Exceptions;

public class ProductOutOfRangeException : Exception
{
    public ProductOutOfRangeException(string message) : base(message) { }
}
