namespace TranslationManagement.Application.Abstractions;

/// <summary>
/// Deserializes content of a file.
/// Deserialization can depend on a different file type.
/// </summary>
public interface IFileDeserializer
{
    Task<string> GetFileContentAsync(Stream fileStream);
}
