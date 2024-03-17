using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Application.Exceptions;

public class InvalidJobFileException : Exception
{
    public InvalidJobFileException() : base() { }

    public InvalidJobFileException(string message) : base(message) { }

    public InvalidJobFileException(string message, Exception innerException) : base(message, innerException) { }
}
