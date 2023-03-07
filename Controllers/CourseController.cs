namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;
using NETCoreDemo.Common;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
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
    [HttpGet]
    public override async Task<ICollection<Course>> GetAll()
    {
        // TODO: Do this but using an extra generic type for the controller and service
        var filter = Request.QueryString.ParseParams<CourseFilterDTO>();
        return await _courseService.GetAllAsync(filter);
    }
}