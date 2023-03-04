namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

public class CourseController : CrudController<Course, CourseDTO>
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService service) : base(service)
    {
        _courseService = service;
    }

    // TODO: Combine this with the GetAll() method from the base class
    // 1. If no status is given on query string, return all
    // 2. Otherwise, filter the courses by status
    [HttpGet("search")]
    public async Task<ICollection<Course>> GetCoursesByStatus([FromQuery] Course.CourseStatus status)
    {
        return await _courseService.GetCoursesByStatusAsync(status);
    }
}