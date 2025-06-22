using Footbook.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Footbook.Core.DTOs.Requests.Stadium;

public record CreateStadiumRequest(
    string Name,
    Branch Branch,
    string Address,
    double Latitude,
    double Longitude,
    IFormFile? Image
);