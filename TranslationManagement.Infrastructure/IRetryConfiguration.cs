using Polly.Retry;

namespace TranslationManagement.Infrastructure;

/// <summary>
/// Provides specific configuration of wait and retry for a polly pipeline.
/// Any other configuration can be added here and used.
/// </summary>
public interface IRetryConfiguration
{
    RetryStrategyOptions ConfigPollyWaitAndRetryPolicy();
}
