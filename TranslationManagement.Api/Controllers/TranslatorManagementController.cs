using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/TranslatorsManagement/[action]")]
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
        public Task<BaseResponse<Translator[]>> GetTranslatorsAsync(CancellationToken cancellationToken)
        {
            return _translatorManagementService.GetTranslatorsAsync(cancellationToken);
        }

        [HttpGet]
        public Task<BaseResponse<Translator[]>> GetTranslatorsByNameAsync(string name, CancellationToken cancellationToken)
        {
            return _translatorManagementService.GetTranslatorsByNameAsync(name, cancellationToken);
        }

        [HttpPost]
        public Task<BaseResponse<bool>> AddTranslatorAsync(Translator translator, CancellationToken cancellationToken)
        {
            return _translatorManagementService.AddTranslatorAsync(translator, cancellationToken);
        }
        
        [HttpPost]
        public Task<BaseResponse<TranslatorStatus>> UpdateTranslatorStatusAsync(int translator, TranslatorStatus newStatus, CancellationToken cancellationToken)
        {
            return _translatorManagementService.UpdateTranslatorStatusAsync(translator, newStatus, cancellationToken);
        }
    }
}