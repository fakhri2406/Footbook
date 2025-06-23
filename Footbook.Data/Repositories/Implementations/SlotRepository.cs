using Microsoft.EntityFrameworkCore;
using Footbook.Data.DataAccess;
using Footbook.Data.Models;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Core.Enums;
using System.Linq;

namespace Footbook.Data.Repositories.Implementations;

public class SlotRepository : ISlotRepository
{
    private readonly AppDbContext _context;
    public SlotRepository(AppDbContext context) => _context = context;
    
    public async Task<Slot> CreateAsync(Slot slot)
    {
        _context.Slots.Add(slot);
        await _context.SaveChangesAsync();
        return slot;
    }
    
    public async Task<IEnumerable<Slot>> GetAllAsync()
    {
        return await _context.Slots
            .Include(s => s.Field)
            .ThenInclude(f => f.Stadium)
            .Include(s => s.Bookings)
            .ToListAsync();
    }
    
    public async Task<Slot?> GetByIdAsync(Guid id)
    {
        return await _context.Slots
            .Include(s => s.Field)
            .ThenInclude(f => f.Stadium)
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<IEnumerable<Slot>> GetByFieldIdAsync(Guid fieldId)
    {
        return await _context.Slots
            .Where(s => s.FieldId == fieldId)
            .Include(s => s.Field)
            .ThenInclude(f => f.Stadium)
            .Include(s => s.Bookings)
            .ToListAsync();
    }
    
    public async Task<Slot> UpdateAsync(Slot slot)
    {
        _context.Slots.Update(slot);
        await _context.SaveChangesAsync();
        return slot;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var slot = await _context.Slots.FindAsync(id);
        if (slot is not null)
        {
            _context.Slots.Remove(slot);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<(IEnumerable<Slot> Items, int TotalCount)> SearchAsync(string? stadiumName, DateTime? date, string? region, bool? onlyOpen, int page, int pageSize)
    {
        var query = _context.Slots
            .Include(s => s.Field)
            .ThenInclude(f => f.Stadium)
            .Include(s => s.Bookings)
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(stadiumName))
        {
            query = query.Where(s => s.Field.Stadium.Name.Contains(stadiumName));
        }
        
        if (date.HasValue)
        {
            query = query.Where(s => s.StartTime.Date == date.Value.Date);
        }
        
        if (!string.IsNullOrWhiteSpace(region))
        {
            query = query.Where(s => s.Field.Stadium.Branch.ToString() == region);
        }
        
        if (onlyOpen.HasValue && onlyOpen.Value)
        {
            query = query.Where(s => s.Status == SlotStatus.Open);
        }
        
        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(s => s.StartTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return (items, totalCount);
    }
} 