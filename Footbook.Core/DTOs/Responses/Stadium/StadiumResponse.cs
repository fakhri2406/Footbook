using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Responses.Stadium;

public record StadiumResponse(
    string Name,
    Branch Branch,
    string Address,
    double Latitude,
    double Longitude,
    string? ImageUrl
);