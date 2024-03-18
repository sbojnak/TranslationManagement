using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Application.Exceptions;
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

    public async Task<bool> CreateJobAsync(TranslationJob job, CancellationToken cancellationToken)
    {
        job.Status = JobStatus.New;
        SetPrice(job);
        _context.TranslationJobs.Add(job);

        bool success = await _context.SaveChangesAsync(cancellationToken) > 0;

        if (success)
        {
            await _messagingService.SendNotificationAsync("Job created: " + job.Id, cancellationToken);
            _logger.LogInformation("New job with ID {JobID} notification sent", job.Id);
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

        var newJob = new TranslationJob()
        {
            OriginalContent = content,
            TranslatedContent = "",
            CustomerName = customer,
        };

        SetPrice(newJob);

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
                                     job.Status == JobStatus.Completed || newStatus == JobStatus.New;
        if (isInvalidStatusChange)
        {
            throw new InvalidJobStatusChangeException($"Status change from {job.Status} to {newStatus} is invalid");
        }

        job.Status = newStatus;
        job.TranslatorId = translatorId;
        await _context.SaveChangesAsync(cancellationToken);
        return job.Status;
    }

    private void SetPrice(TranslationJob job)
    {
        job.Price = job.OriginalContent.Length * PricePerCharacter;
    }
}
