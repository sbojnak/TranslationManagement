using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Application.Exceptions;
using TranslationManagement.Domain.DataTransferObjects;
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

    public async Task<TranslatorDto[]> GetTranslatorsAsync(CancellationToken cancellationToken)
    {
        var resultEntities = await _context.Translators.ToArrayAsync(cancellationToken);
        
        return resultEntities.Select(x => new TranslatorDto(
            Id: x.Id,
            Name: x.Name,
            HourlyRate: x.HourlyRate,
            Status: x.Status,
            CreditCardNumber: x.CreditCardNumber))
            .ToArray();
    }

    public async Task<TranslatorDto[]> GetTranslatorsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var resultEntities = await _context.Translators.Where(t => t.Name == name).ToArrayAsync(cancellationToken);
        return resultEntities.Select(x => new TranslatorDto(
            Id: x.Id,
            Name: x.Name,
            HourlyRate: x.HourlyRate,
            Status: x.Status,
            CreditCardNumber: x.CreditCardNumber))
            .ToArray();

    }

    public async Task<bool> AddTranslatorAsync(TranslatorDto translator, CancellationToken cancellationToken)
    {
        _context.Translators.Add(new Translator 
        {
            Status = translator.Status,
            CreditCardNumber = translator.CreditCardNumber,
            HourlyRate = translator.HourlyRate,
            Name = translator.Name
        });
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<TranslatorStatus> UpdateTranslatorStatusAsync(int translatorId, TranslatorStatus newStatus, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User status update request: " + newStatus + " for user " + translatorId.ToString());

        var translator = await _context.Translators.FirstOrDefaultAsync(t => t.Id == translatorId);

        if(translator == null)
        {
            throw new InvalidTranslatorIdException($"Cannot find translator ID {translatorId} in the database");
        }

        translator.Status = newStatus;
        await _context.SaveChangesAsync(cancellationToken);

        return translator.Status;
    }
}
