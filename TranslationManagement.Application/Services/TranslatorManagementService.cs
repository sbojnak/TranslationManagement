using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Application.Services;

internal class TranslatorManagementService : ITranslatorManagementService
{
    private readonly IDbContext _context;
    private readonly ILogger<TranslatorManagementService> _logger;

    public TranslatorManagementService(IDbContext context, ILogger<TranslatorManagementService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<Translator[]> GetTranslatorsAsync(CancellationToken cancellationToken)
    {
        return _context.Translators.ToArrayAsync(cancellationToken);
    }

    public Task<Translator[]> GetTranslatorsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _context.Translators.Where(t => t.Name == name).ToArrayAsync(cancellationToken);
    }

    public async Task<bool> AddTranslatorAsync(Translator translator, CancellationToken cancellationToken)
    {
        _context.Translators.Add(translator);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<TranslatorStatus> UpdateTranslatorStatusAsync(int translatorId, TranslatorStatus newStatus, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User status update request: " + newStatus + " for user " + translatorId.ToString());

        var translator = _context.Translators.Single(t => t.Id == translatorId);
        translator.Status = newStatus;
        await _context.SaveChangesAsync(cancellationToken);

        return translator.Status;
    }
}
