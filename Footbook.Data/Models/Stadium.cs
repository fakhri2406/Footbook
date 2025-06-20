using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class Stadium
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    public Branch Branch { get; set; }
    public string Address { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public ICollection<Field> Fields { get; set; } = new List<Field>();
} 