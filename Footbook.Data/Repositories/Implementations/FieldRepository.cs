using Microsoft.EntityFrameworkCore;
using Footbook.Data.DataAccess;
using Footbook.Data.Models;
using Footbook.Data.Repositories.Interfaces;

namespace Footbook.Data.Repositories.Implementations;

public class FieldRepository : IFieldRepository
{
    private readonly AppDbContext _context;
    public FieldRepository(AppDbContext context) => _context = context;
    
    public async Task<Field> CreateAsync(Field field)
    {
        _context.Fields.Add(field);
        await _context.SaveChangesAsync();
        return field;
    }
    
    public async Task<IEnumerable<Field>> GetAllAsync()
    {
        return await _context.Fields
            .Include(f => f.Stadium)
            .ToListAsync();
    }
    
    public async Task<Field?> GetByIdAsync(Guid id)
    {
        return await _context.Fields
            .Include(f => f.Stadium)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    
    public async Task<IEnumerable<Field>> GetByStadiumIdAsync(Guid stadiumId)
    {
        return await _context.Fields
            .Where(f => f.StadiumId == stadiumId)
            .Include(f => f.Stadium)
            .ToListAsync();
    }
    
    public async Task<Field> UpdateAsync(Field field)
    {
        _context.Fields.Update(field);
        await _context.SaveChangesAsync();
        return field;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var field = await _context.Fields.FindAsync(id);
        if (field is not null)
        {
            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();
        }
    }
} 