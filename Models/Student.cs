using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace NETCoreDemo.Models;
using System.Text.Json.Serialization;

public class Student : BaseModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [NotMapped]
    [JsonIgnore]
    public string FullName
    {
        get => $"{FirstName} {LastName}";
    }

    [MaxLength(256)]
    public string Email { get; set; } = null!;

    // Mark the related entity as virtual to use lazy loading
    public Address? Address { get; set; }

    [JsonIgnore]
    public int? AddressId { get; set; }

    // TODO: This will cause JSON object cycle issue, find a solution to fix it
    public Course Course { get; set; } = null!;

    public int CourseId { get; set; }

    // Direct many-to-many
    public ICollection<Assignment> Assignments { get; set; } = null!;

    // Indirect many-to-many
    public ICollection<ProjectStudent> ProjectLinks { get; set; } = null!;
}