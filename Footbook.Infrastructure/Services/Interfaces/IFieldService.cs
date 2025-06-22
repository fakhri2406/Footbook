using Footbook.Core.DTOs.Requests.Field;
using Footbook.Core.DTOs.Responses.Field;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IFieldService
{
    Task<FieldResponse> CreateAsync(CreateFieldRequest request);
    Task<IEnumerable<FieldResponse>> GetAllAsync();
    Task<FieldResponse> GetByIdAsync(Guid id);
    Task<FieldResponse> UpdateAsync(Guid id, UpdateFieldRequest request);
    Task DeleteAsync(Guid id);
} 