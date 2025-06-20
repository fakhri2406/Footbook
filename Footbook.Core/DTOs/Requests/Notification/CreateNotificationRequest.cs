using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Requests.Notification;

public record CreateNotificationRequest(
    NotificationType Type,
    string Payload,
    Guid UserId
); 