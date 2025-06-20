using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Requests.Field;

public record UpdateFieldRequest(string Name, FieldType FieldType, Guid StadiumId, int Capacity = 12);