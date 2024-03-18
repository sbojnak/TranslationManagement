using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Application.Exceptions;
using TranslationManagement.Domain.DataTransferObjects;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Application.Services;

internal class TranslationJobService : ITranslationJobService
{
    private const double PricePerCharacter = 0.01;
    private readonly IDbContext _context;
    private readonly ILogger<TranslationJobService> _logger;
    private readonly IMessagingService _messagingService;
    private readonly IFileDeserializerTypeResolver _fileDeserializerTypeResolver;

    public TranslationJobService(IDbContext context,
        ILogger<TranslationJobService> logger,
        IMessagingService messagingService,
        IFileDeserializerTypeResolver fileDeserializerTypeResolver)
    {
        _context = context;
        _logger = logger;
        _messagingService = messagingService;
        _fileDeserializerTypeResolver = fileDeserializerTypeResolver;
    }

    public Task<TranslationJob[]> GetJobsAsync(CancellationToken cancellationToken)
    {
        return _context.TranslationJobs
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<bool> CreateJobAsync(TranslationJobDto job, CancellationToken cancellationToken)
    {
        var jobEntity = new TranslationJob
        {
            CustomerName = job.CustomerName,
            Status = JobStatus.New,
            OriginalContent = job.OriginalContent,
            Price = SetPrice(job.OriginalContent.Length),
            TranslatedContent = job.TranslatedContent,
        };
        _context.TranslationJobs.Add(jobEntity);

        bool success = await _context.SaveChangesAsync(cancellationToken) > 0;

        if (success)
        {
            var _ = Task.Run(() => _messagingService.SendNotificationAsync("Job created: " + jobEntity.Id, cancellationToken), cancellationToken);
            _logger.LogInformation("New job with ID {JobID} notification sent", jobEntity.Id);
        }

        return success;
    }

    public async Task<bool> CreateJobWithFileAsync(Stream streamOfFile, string filename, string customer, CancellationToken cancellationToken)
    {
        var fileExtension = Path.GetExtension(filename);

        IFileDeserializer deserializer = _fileDeserializerTypeResolver.GetFileDeserializerByType(fileExtension);

        var content = await deserializer.GetFileContentAsync(streamOfFile);

        if (content is null)
        {
            throw new ArgumentNullException("Content of file {Filename} is null", filename);
        }

        var newJob = new TranslationJobDto(
            Id: default,
            CustomerName: customer,
            OriginalContent: content,
            TranslatedContent: "",
            Price: SetPrice(content.Length)
        );

        return await CreateJobAsync(newJob, cancellationToken);
    }

    public async Task<JobStatus> UpdateJobStatusAsync(int jobId, int translatorId, JobStatus newStatus, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Job status update request received: " + newStatus + " for job " + jobId.ToString() + " by translator " + translatorId);

        var job = await _context.TranslationJobs.FirstOrDefaultAsync(j => j.Id == jobId);

        if(job == null)
        {
            throw new InvalidJobIdException($"Cannot find job ID {jobId} in the database");
        }

        var translator = _context.Translators.Single(t => t.Id == translatorId);

        if(translator == null)
        {
            throw new InvalidTranslatorIdException($"Cannot find translator ID {translatorId} in the database");
        }

        bool isInvalidStatusChange = (job.Status == JobStatus.New && newStatus == JobStatus.Completed) ||
                                     job.Status == JobStatus.Completed || 
                                     newStatus == JobStatus.New ||
                                     translator.Status != TranslatorStatus.Certified;
                                     
        if (isInvalidStatusChange)
        {
            throw new InvalidJobStatusChangeException($"Status change from {job.Status} to {newStatus} is invalid for translator with id {translator.Id}. " +
                                                      $"Transition is incorrect or translator is not certified.");
        }

        job.Status = newStatus;
        job.TranslatorId = translatorId;
        await _context.SaveChangesAsync(cancellationToken);
        return job.Status;
    }

    private double SetPrice(int jobOriginalContentLength)
    {
        return jobOriginalContentLength * PricePerCharacter;
    }
}
