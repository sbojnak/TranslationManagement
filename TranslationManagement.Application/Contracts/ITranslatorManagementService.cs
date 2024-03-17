using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Application.Contracts;

public interface ITranslatorManagementService
{
    Task<bool> AddTranslatorAsync(Translator translator, CancellationToken cancellationToken);
    Task<Translator[]> GetTranslatorsAsync(CancellationToken cancellationToken);
    Task<Translator[]> GetTranslatorsByNameAsync(string name, CancellationToken cancellationToken);
    Task<TranslatorStatus> UpdateTranslatorStatusAsync(int translatorId, TranslatorStatus newStatus, CancellationToken cancellationToken);
}