using TranslationManagement.Domain.DataTransferObjects;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Application.Contracts;

/// <summary>
/// Provides business functionality for translator management
/// </summary>
public interface ITranslatorManagementService
{
    Task<bool> AddTranslatorAsync(TranslatorDto translator, CancellationToken cancellationToken);
    Task<TranslatorDto[]> GetTranslatorsAsync(CancellationToken cancellationToken);
    Task<TranslatorDto[]> GetTranslatorsByNameAsync(string name, CancellationToken cancellationToken);
    Task<TranslatorStatus> UpdateTranslatorStatusAsync(int translatorId, TranslatorStatus newStatus, CancellationToken cancellationToken);
}