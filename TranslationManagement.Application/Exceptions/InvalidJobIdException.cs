using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Application.Exceptions;

/// <summary>
/// Raised when job id is not valid.
/// </summary>
public class InvalidJobIdException : Exception
{
    public InvalidJobIdException() : base() { }

    public InvalidJobIdException(string message) : base(message) { }

    public InvalidJobIdException(string message, Exception innerException) : base(message, innerException) { }

}
