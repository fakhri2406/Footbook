using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class Field
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    public FieldType FieldType { get; set; }
    public int Capacity { get; set; } = 12;
    
    public Guid StadiumId { get; set; }
    public Stadium Stadium { get; set; } = null!;
    
    public ICollection<TimeWindow> TimeWindows { get; set; } = new List<TimeWindow>();
} 