using Footbook.Core.DTOs.Requests.Notification;
using Footbook.Core.DTOs.Responses.Notification;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface INotificationService
{
    Task<NotificationResponse> CreateAsync(CreateNotificationRequest request);
    Task<IEnumerable<NotificationResponse>> GetByUserIdAsync(Guid userId);
    Task MarkAsReadAsync(Guid id);
    Task DeleteAsync(Guid id);
} 