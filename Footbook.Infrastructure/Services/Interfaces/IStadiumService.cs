using Footbook.Core.DTOs.Requests.Stadium;
using Footbook.Core.DTOs.Responses.Stadium;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IStadiumService
{
    Task<StadiumResponse> CreateAsync(CreateStadiumRequest request);
    Task<IEnumerable<StadiumResponse>> GetAllAsync();
    Task<StadiumResponse> GetByIdAsync(Guid id);
    Task<StadiumResponse> UpdateAsync(Guid id, UpdateStadiumRequest request);
    Task DeleteAsync(Guid id);
} 