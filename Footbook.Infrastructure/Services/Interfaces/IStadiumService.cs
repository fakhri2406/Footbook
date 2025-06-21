using Footbook.Core.DTOs.Requests.Stadium;
using Footbook.Core.DTOs.Responses.Stadium;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IStadiumService
{
    Task<CreateStadiumResponse> CreateAsync(CreateStadiumRequest request);
    Task<IEnumerable<CreateStadiumResponse>> GetAllAsync();
    Task<CreateStadiumResponse> GetByIdAsync(Guid id);
    Task<UpdateStadiumResponse> UpdateAsync(Guid id, UpdateStadiumRequest request);
    Task DeleteAsync(Guid id);
} 