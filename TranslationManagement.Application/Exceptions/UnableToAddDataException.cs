namespace TranslationManagement.Application.Exceptions;

/// <summary>
/// Exception to be raised when data cannot be added to the database
/// </summary>
public class UnableToAddDataException : Exception
{
    public UnableToAddDataException() : base() { }

    public UnableToAddDataException(string message) : base(message) { }

    public UnableToAddDataException(string message, Exception innerException) : base(message, innerException) { }
}
