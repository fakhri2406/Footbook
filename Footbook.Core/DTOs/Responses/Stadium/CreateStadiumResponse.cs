using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Responses.Stadium;

public record CreateStadiumResponse(
    string Name,
    Branch Branch,
    string Address,
    double Latitude,
    double Longitude
);