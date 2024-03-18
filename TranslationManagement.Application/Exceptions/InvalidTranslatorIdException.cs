using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Application.Exceptions;

/// <summary>
/// Raised when job id is not valid.
/// </summary>
public class InvalidTranslatorIdException : Exception
{
    public InvalidTranslatorIdException() : base() { }

    public InvalidTranslatorIdException(string message) : base(message) { }

    public InvalidTranslatorIdException(string message, Exception innerException) : base(message, innerException) { }

}
