using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Application.Exceptions;

public class InvalidJobStatusChangeException : Exception
{
    public InvalidJobStatusChangeException() : base() { }

    public InvalidJobStatusChangeException(string message) : base(message) { }

    public InvalidJobStatusChangeException(string message, Exception innerException) : base(message, innerException) { }
}
