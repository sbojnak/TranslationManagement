namespace TranslationManagement.Application.Exceptions;

/// <summary>
/// Exception to be raised when job transition from old status to new status is not valid.
/// </summary>
public class InvalidJobStatusChangeException : Exception
{
    public InvalidJobStatusChangeException() : base() { }

    public InvalidJobStatusChangeException(string message) : base(message) { }

    public InvalidJobStatusChangeException(string message, Exception innerException) : base(message, innerException) { }
}
