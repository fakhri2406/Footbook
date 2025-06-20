using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Requests.Slot;

public record CreateSlotRequest(
    DateTime StartTime,
    DateTime EndTime,
    Guid FieldId,
    SlotStatus Status = SlotStatus.Open
);