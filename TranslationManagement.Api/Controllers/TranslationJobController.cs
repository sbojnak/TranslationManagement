using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Asp.Versioning;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Application.Exceptions;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/jobs")]
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
    [Route("Get")]
    public Task<TranslationJob[]> GetJobsAsync(CancellationToken cancellationToken)
    {
        return _translationJobService.GetJobsAsync(cancellationToken);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateJobAsync([FromBody] TranslationJob job, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Going to create job with Id {JobId}", job.Id);
        
        var success = await _translationJobService.CreateJobAsync(job, cancellationToken);

        _logger.LogInformation("Job with id {JobId} successfully created", job.Id);

        if (success)
        {
            return Ok();
        }
        throw new UnableToAddDataException("Unable to create a new job.");
    }

    [HttpPost]
    [Route("Create/{customerName}")]
    public async Task<IActionResult> CreateJobWithFileAsync(IFormFile file, [FromRoute] string customerName, CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();
        var success = await _translationJobService.CreateJobWithFileAsync(stream, file.Name, customerName, cancellationToken);
        if (success)
        {
            return Ok();
        }
        throw new UnableToAddDataException("Unable to create a new job.");
    }

    [HttpPut]
    [Route("{jobId}/UpdateStatus")]
    public Task<JobStatus> UpdateJobStatusAsync([FromRoute] int jobId, [FromQuery] int newTranslatorId, [FromQuery] JobStatus newStatus, CancellationToken cancellationToken)
    {
        if(jobId <= 0)
        {
            throw new InvalidJobIdException($"Job id value is {jobId}. It has to be more than zero.");
        }

        return _translationJobService.UpdateJobStatusAsync(jobId, newTranslatorId, newStatus, cancellationToken);
    }
}