
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.Entities;

public class Translator
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal HourlyRate { get; set; }
    public TranslatorStatus Status { get; set; }
    public string CreditCardNumber { get; set; } = null!;
    public List<TranslationJob> TranslationJobs { get; set; } = new();
}
