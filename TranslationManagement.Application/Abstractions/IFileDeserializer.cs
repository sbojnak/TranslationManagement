using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Application.Abstractions
{
    public interface IFileDeserializer
    {
        Task<string> GetFileContentAsync(Stream fileStream);
    }
}
