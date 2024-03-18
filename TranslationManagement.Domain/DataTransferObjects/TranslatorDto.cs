using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.DataTransferObjects;

public record TranslatorDto(int Id, string Name, decimal HourlyRate, TranslatorStatus Status, string CreditCardNumber);

