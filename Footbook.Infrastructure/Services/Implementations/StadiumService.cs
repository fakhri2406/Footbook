using Footbook.Core.DTOs.Requests.Stadium;
using Footbook.Core.DTOs.Responses.Stadium;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class StadiumService : IStadiumService
{
    private readonly IStadiumRepository _stadiumRepository;
    
    public StadiumService(IStadiumRepository stadiumRepository)
        => _stadiumRepository = stadiumRepository;
    
    public async Task<CreateStadiumResponse> CreateAsync(CreateStadiumRequest request)
    {
        var stadium = request.MapToStadium();
        var created = await _stadiumRepository.CreateAsync(stadium);
        return created.MapToCreateStadiumResponse();
    }
    
    public async Task<IEnumerable<CreateStadiumResponse>> GetAllAsync()
    {
        var stadiums = await _stadiumRepository.GetAllAsync();
        return stadiums.Select(s => s.MapToCreateStadiumResponse());
    }
    
    public async Task<CreateStadiumResponse> GetByIdAsync(Guid id)
    {
        var stadium = await _stadiumRepository.GetByIdAsync(id);
        
        if (stadium is null)
        {
            throw new KeyNotFoundException("Stadium not found.");
        }
        
        return stadium.MapToCreateStadiumResponse();
    }

    public async Task<UpdateStadiumResponse> UpdateAsync(Guid id, UpdateStadiumRequest request)
    {
        var stadium = request.MapToStadium(id);
        var updated = await _stadiumRepository.UpdateAsync(stadium);
        return updated.MapToUpdateStadiumResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _stadiumRepository.DeleteAsync(id);
    }
} 