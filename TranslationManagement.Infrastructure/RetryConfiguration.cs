using Polly.Retry;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Infrastructure
{
    internal class RetryConfiguration : IRetryConfiguration
    {
        private readonly ILogger<RetryConfiguration> _logger;

        public RetryConfiguration(ILogger<RetryConfiguration> logger) 
        {
            _logger = logger;
        }

        /// <summary>
        ///=There is a 98.24% chance that this will be successful
        /// Change to different strategy if the percentage is not enough for SLA or the real percentage is different.
        /// </summary>
        /// <returns></returns>
        public RetryStrategyOptions ConfigPollyWaitAndRetryPolicy()
        {
            return new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder()
                    .Handle<ApplicationException>()
                    .HandleResult(false),
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true,  // Adds a random factor to the delay
                MaxRetryAttempts = 10,
                Delay = TimeSpan.FromSeconds(1),
                OnRetry = (retryArguments) =>
                {
                    _logger.LogError($"Failed attempt to send notification. " +
                        "Waited for {CalculatedWaitDuration} in attempt number {AttemtNumber}.",
                        retryArguments.Duration,
                        retryArguments.AttemptNumber);

                    return default;
                }
            };
        }
    }
}
