using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.Entities;

public class TranslationJob
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public JobStatus Status { get; set; }
    public string OriginalContent { get; set; } = null!;
    public string? TranslatedContent { get; set; }
    public double Price { get; set; }
    public int? TranslatorId { get; set; }
    public Translator? Translator { get; set; }
}
