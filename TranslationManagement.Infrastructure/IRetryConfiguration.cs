using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Infrastructure
{
    public interface IRetryConfiguration
    {
        RetryStrategyOptions ConfigPollyWaitAndRetryPolicy();
    }
}
