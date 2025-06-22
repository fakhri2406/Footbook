using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Responses.Slot;

public record SlotResponse(DateTime StartTime, DateTime EndTime, SlotStatus Status, Guid FieldId);