using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Requests.Stadium;

public record UpdateStadiumRequest(
    string Name,
    Branch Branch,
    string Address,
    double Latitude,
    double Longitude
);