using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs/[action]")]
    public class TranslationJobController : ControllerBase
    {
        private readonly ILogger<TranslatorManagementController> _logger;
        private readonly ITranslationJobService _translationJobService;

        public TranslationJobController(ILogger<TranslatorManagementController> logger,
            ITranslationJobService translationJobService)
        {
            _logger = logger;
            _translationJobService = translationJobService;
        }

        [HttpGet]
        public Task<BaseResponse<TranslationJob[]>> GetJobsAsync(CancellationToken cancellationToken)
        {
            return _translationJobService.GetJobsAsync(cancellationToken);
        }

        [HttpPost]
        public Task<BaseResponse<bool>> CreateJobAsync(TranslationJob job, CancellationToken cancellationToken)
        {
            return _translationJobService.CreateJobAsync(job, cancellationToken);
        }

        [HttpPost]
        public Task<BaseResponse<bool>> CreateJobWithFileAsync(IFormFile file, string customer, CancellationToken cancellationToken)
        {
            using var stream = file.OpenReadStream();
            return _translationJobService.CreateJobWithFileAsync(stream, file.Name, customer, cancellationToken);
        }

        [HttpPost]
        public Task<BaseResponse<JobStatus>> UpdateJobStatusAsync(int jobId, int translatorId, JobStatus newStatus, CancellationToken cancellationToken)
        {
            return _translationJobService.UpdateJobStatusAsync(jobId, translatorId, newStatus, cancellationToken);
        }
    }
}