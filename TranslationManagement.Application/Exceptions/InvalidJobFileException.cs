namespace TranslationManagement.Application.Exceptions;

/// <summary>
/// Exception to be raised when file provided for a translation job is not valid.
/// </summary>
public class InvalidJobFileException : Exception
{
    public InvalidJobFileException() : base() { }

    public InvalidJobFileException(string message) : base(message) { }

    public InvalidJobFileException(string message, Exception innerException) : base(message, innerException) { }
}
