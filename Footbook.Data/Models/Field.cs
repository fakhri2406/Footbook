using System.ComponentModel.DataAnnotations;
using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class Field
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    public FieldType FieldType { get; set; }
    public int Capacity { get; set; }
    public string? ImageUrl { get; set; }
    
    public Guid StadiumId { get; set; }
    public Stadium Stadium { get; set; } = null!;
    
    public ICollection<Slot> TimeWindows { get; set; } = new List<Slot>();
} 