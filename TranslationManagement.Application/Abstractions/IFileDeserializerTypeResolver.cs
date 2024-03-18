namespace TranslationManagement.Application.Abstractions;

/// <summary>
/// Decides about what deserializer will be used by file type. File type should contain also a '.' prefix.
/// </summary>
public interface IFileDeserializerTypeResolver
{
    IFileDeserializer GetFileDeserializerByType(string fileType);
}
