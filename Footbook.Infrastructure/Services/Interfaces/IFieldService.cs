using Footbook.Core.DTOs.Requests.Field;
using Footbook.Core.DTOs.Responses.Field;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IFieldService
{
    Task<CreateFieldResponse> CreateAsync(CreateFieldRequest request);
    Task<IEnumerable<CreateFieldResponse>> GetAllAsync();
    Task<CreateFieldResponse> GetByIdAsync(Guid id);
    Task<UpdateFieldResponse> UpdateAsync(Guid id, UpdateFieldRequest request);
    Task DeleteAsync(Guid id);
} 