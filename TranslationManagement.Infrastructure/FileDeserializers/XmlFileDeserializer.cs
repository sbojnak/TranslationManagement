using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Application.Exceptions;

namespace TranslationManagement.Infrastructure.FileDeserializers
{
    internal class XmlFileDeserializer : IFileDeserializer
    {
        public async Task<string> GetFileContentAsync(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);
            var fullFileContent = await reader.ReadToEndAsync();

            var xdoc = XDocument.Parse(fullFileContent);
            return xdoc.Root?.Element("Content")?.Value ?? throw new InvalidJobFileException("XML file does not contain valid content");
        }
    }
}
