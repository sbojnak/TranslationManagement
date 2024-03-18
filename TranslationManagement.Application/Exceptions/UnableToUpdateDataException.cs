namespace TranslationManagement.Application.Exceptions;

/// <summary>
/// Exception to be raised when data in the database cannot be updated.
/// </summary>
public class UnableToUpdateDataException : Exception
{
    public UnableToUpdateDataException() : base() { }

    public UnableToUpdateDataException(string message) : base(message) { }

    public UnableToUpdateDataException(string message, Exception innerException) : base(message, innerException) { }
}
