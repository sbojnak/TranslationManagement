using System.ComponentModel.DataAnnotations;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.Entities;

public class TranslationJob : IValidatableObject
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public JobStatus Status { get; set; }
    public string OriginalContent { get; set; } = null!;
    public string? TranslatedContent { get; set; }
    public double Price { get; set; }
    public int? TranslatorId { get; set; }
    public Translator? Translator { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Translator != null && Translator.Status != TranslatorStatus.Certified && Status != JobStatus.New)
        {
            yield return new ValidationResult(
                "Translator with ID is not certified to work on this job",
                new[] { nameof(Translator), nameof(Status) });
        }
    }
}
