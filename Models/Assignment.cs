namespace NETCoreDemo.Models;

public class Assignment : BaseModel
{
    public DateTime Deadline { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = null!;
}