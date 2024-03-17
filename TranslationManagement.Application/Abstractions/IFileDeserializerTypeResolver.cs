using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Application.Abstractions
{
    public interface IFileDeserializerTypeResolver
    {
        IFileDeserializer GetFileDeserializerByType(string fileType);
    }
}
