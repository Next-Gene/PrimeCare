namespace eCommerceApp.Application.Exceptions;

/// <summary>
/// Exception that is thrown when an item is not found.
/// </summary>
public class ItemNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ItemNotFoundException(string message)
        : base(message)
    {

    }
}
