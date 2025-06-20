using Microsoft.EntityFrameworkCore;
using Footbook.Data.DataAccess;
using Footbook.Data.Models;
using Footbook.Data.Repositories.Interfaces;

namespace Footbook.Data.Repositories.Implementations;

public class StadiumRepository : IStadiumRepository
{
    private readonly AppDbContext _context;
    public StadiumRepository(AppDbContext context) => _context = context;

    public async Task<Stadium> CreateAsync(Stadium stadium)
    {
        _context.Stadiums.Add(stadium);
        await _context.SaveChangesAsync();
        return stadium;
    }
    
    public async Task<IEnumerable<Stadium>> GetAllAsync()
    {
        return await _context.Stadiums
            .Include(s => s.Fields)
            .ToListAsync();
    }
    
    public async Task<Stadium?> GetByIdAsync(Guid id)
    {
        return await _context.Stadiums
            .Include(s => s.Fields)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<Stadium> UpdateAsync(Stadium stadium)
    {
        _context.Stadiums.Update(stadium);
        await _context.SaveChangesAsync();
        return stadium;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var stadium = await _context.Stadiums.FindAsync(id);
        if (stadium is not null)
        {
            _context.Stadiums.Remove(stadium);
            await _context.SaveChangesAsync();
        }
    }
} 