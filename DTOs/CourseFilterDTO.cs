namespace NETCoreDemo.DTOs;

using NETCoreDemo.Models;

public class CourseFilterDTO : ICrudFilter
{
    public Course.CourseStatus Status { get; set; }
    public string? Keyword { get; set; }
}