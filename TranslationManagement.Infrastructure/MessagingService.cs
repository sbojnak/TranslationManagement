using Polly.Retry;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using External.ThirdParty.Services;
using System.Threading;
using TranslationManagement.Application.Abstractions;

namespace TranslationManagement.Infrastructure
{
    public class MessagingService : IMessagingService
    {
        private readonly ILogger<MessagingService> _logger;
        private readonly ResiliencePipeline _pipeline;
        private readonly UnreliableNotificationService _notificationService;

        public MessagingService(ILogger<MessagingService> logger,
            IRetryConfiguration retryConfiguration,
            UnreliableNotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _pipeline = new ResiliencePipelineBuilder()
                .AddRetry(retryConfiguration.ConfigPollyWaitAndRetryPolicy())
                .Build();
        }

        public async Task SendNotificationAsync(string message, CancellationToken cancellationToken)
        {
            await _pipeline.ExecuteAsync(async context => await _notificationService.SendNotification(message), cancellationToken);
        }
    }
}
