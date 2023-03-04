using System.ComponentModel.DataAnnotations.Schema;
namespace NETCoreDemo.Models;

public class Project : BaseModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "jsonb")]
    public ICollection<string> Tags { get; set; } = null!;

    public DateTime Deadline { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<ProjectStudent> StudentLinks { get; set; } = null!;
}