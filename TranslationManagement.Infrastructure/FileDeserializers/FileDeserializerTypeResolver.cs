using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Application.Exceptions;

namespace TranslationManagement.Infrastructure.FileDeserializers
{
    internal class FileDeserializerTypeResolver : IFileDeserializerTypeResolver
    {
        public IFileDeserializer GetFileDeserializerByType(string fileType)
        {
            return fileType switch
            {
                ".txt" => new TxtFileDeserializer(), //TODO: can be retrieved through DI container in the future
                ".xml" => new XmlFileDeserializer(),
                _ => throw new InvalidJobFileException("Invalid file type")
            };
        }
    }
}
