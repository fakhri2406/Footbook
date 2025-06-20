using Footbook.Core.DTOs.Requests.Notification;
using Footbook.Core.DTOs.Responses.Notification;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    
    public NotificationService(INotificationRepository notificationRepository)
        => _notificationRepository = notificationRepository;
    
    public async Task<NotificationResponse> CreateAsync(CreateNotificationRequest request)
    {
        var notification = request.MapToNotification();
        var created = await _notificationRepository.CreateAsync(notification);
        return created.MapToNotificationResponse();
    }
    
    public async Task<IEnumerable<NotificationResponse>> GetByUserIdAsync(Guid userId)
    {
        var notifications = await _notificationRepository.GetByUserIdAsync(userId);
        return notifications.Select(n => n.MapToNotificationResponse());
    }
    
    public async Task MarkAsReadAsync(Guid id)
    {
        await _notificationRepository.MarkAsReadAsync(id);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _notificationRepository.DeleteAsync(id);
    }
} 