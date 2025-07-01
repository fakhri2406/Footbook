namespace Footbook.Core.DTOs.Requests.Slot;

public record SlotSearchRequest(
    string? StadiumName = null,
    DateTime? Date = null,
    string? Region = null,
    bool? OnlyOpen = null,
    int Page = 1,
    int PageSize = 10
); 