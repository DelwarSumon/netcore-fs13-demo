namespace NETCoreDemo.Models;

public class ProjectStudent
{
    public Student Student { get; set; } = null!;
    public Project Project { get; set; } = null!;
    public DateTime JoinedAt { get; set; }
    public ProjectRole Role { get; set; }
    public int StudentId { get; set; }
    public int ProjectId { get; set; }
}