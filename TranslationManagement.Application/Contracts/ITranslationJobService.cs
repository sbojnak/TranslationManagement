using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Application.Contracts;

/// <summary>
/// Provides business functionality for translation job management
/// </summary>
public interface ITranslationJobService
{
    Task<bool> CreateJobAsync(TranslationJob job, CancellationToken cancellationToken);
    Task<bool> CreateJobWithFileAsync(Stream fileStream, string filename, string customer, CancellationToken cancellationToken);
    Task<TranslationJob[]> GetJobsAsync(CancellationToken cancellationToken);
    Task<JobStatus> UpdateJobStatusAsync(int jobId, int translatorId, JobStatus newStatus, CancellationToken cancellationToken);
}