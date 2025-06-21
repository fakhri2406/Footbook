using System.ComponentModel.DataAnnotations;

namespace Footbook.Data.Models;

public class Role
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
} 