using Footbook.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Footbook.Core.DTOs.Requests.Field;

public record UpdateFieldRequest(
    string Name,
    FieldType FieldType,
    Guid StadiumId,
    IFormFile? Image,
    int Capacity = 12
);