using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Responses.Notification;

public record NotificationResponse(
    Guid Id,
    NotificationType Type,
    string Payload,
    bool IsRead,
    DateTime CreatedAt
); 