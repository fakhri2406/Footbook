using Footbook.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Footbook.Core.DTOs.Requests.Field;

public record CreateFieldRequest(
    string Name,
    FieldType FieldType,
    Guid StadiumId,
    IFormFile? Image,
    int Capacity = 12
);