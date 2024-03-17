using TranslationManagement.Application.Abstractions;

namespace TranslationManagement.Infrastructure.FileDeserializers;

internal class TxtFileDeserializer : IFileDeserializer
{
    public async Task<string> GetFileContentAsync(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        return await reader.ReadToEndAsync();
    }
}
