using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationManagement.Application.Abstractions
{
    public interface IMessagingService
    {
        Task SendNotificationAsync(string message, CancellationToken cancellationToken);
    }
}
