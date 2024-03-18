using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Application.Exceptions;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.Controlers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/TranslatorsManagement")]
public class TranslatorManagementController : ControllerBase
{
    private readonly ILogger<TranslatorManagementController> _logger;
    private readonly ITranslatorManagementService _translatorManagementService;

    public TranslatorManagementController(IServiceScopeFactory scopeFactory, 
        ILogger<TranslatorManagementController> logger,
        ITranslatorManagementService translatorManagementService)
    {
        _translatorManagementService = translatorManagementService;
        _logger = logger;
    }

    [HttpGet]
    public Task<Translator[]> GetTranslatorsAsync(CancellationToken cancellationToken)
    {
        return _translatorManagementService.GetTranslatorsAsync(cancellationToken);
    }

    [HttpGet]
    [Route("GetTranslators/{name}")]
    public Task<Translator[]> GetTranslatorsByNameAsync([FromRoute] string name, CancellationToken cancellationToken)
    {
        return _translatorManagementService.GetTranslatorsByNameAsync(name, cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> AddTranslatorAsync([FromBody] Translator translator, CancellationToken cancellationToken)
    {
        var success = await _translatorManagementService.AddTranslatorAsync(translator, cancellationToken);
        if (success)
        {
            return Ok();
        }
        throw new UnableToAddDataException("Cannot add translator");
    }
    
    [HttpPut]
    [Route("UpdateTranslator/{translatorId}")]
    public Task<TranslatorStatus> UpdateTranslatorStatusAsync([FromRoute] int translatorId, [FromQuery] TranslatorStatus newStatus, CancellationToken cancellationToken)
    {
        return _translatorManagementService.UpdateTranslatorStatusAsync(translatorId, newStatus, cancellationToken);
    }
}