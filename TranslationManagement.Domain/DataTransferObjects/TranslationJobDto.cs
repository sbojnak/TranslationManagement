using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.DataTransferObjects;

public record TranslationJobDto(int Id, string CustomerName, string OriginalContent, string? TranslatedContent, double Price);

