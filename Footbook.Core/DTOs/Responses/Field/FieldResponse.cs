using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Responses.Field;

public record FieldResponse(
    string Name,
    FieldType FieldType,
    Guid StadiumId,
    int Capacity,
    string? ImageUrl
);