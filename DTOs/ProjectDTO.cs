using System.Net.NetworkInformation;
namespace NETCoreDemo.DTOs;

using NETCoreDemo.Models;

public class ProjectDTO : BaseDTO<Project>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<string> Tags { get; set; } = null!;
    public DateTime Deadline { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public override void UpdateModel(Project model)
    {
        model.Name = Name;
        model.Description = Description;
        model.Tags = Tags;
        model.Deadline = Deadline;
        model.StartDate = StartDate;
        model.EndDate = EndDate;
    }
}