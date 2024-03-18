namespace TranslationManagement.Application.Abstractions;

/// <summary>
/// Abstraction interface for messaging functionality.
/// </summary>
public interface IMessagingService
{
    Task SendNotificationAsync(string message, CancellationToken cancellationToken);
}
