using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Responses.Field;

public record UpdateFieldResponse(string Name, FieldType FieldType, Guid StadiumId, int Capacity);