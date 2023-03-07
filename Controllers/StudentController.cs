namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Authorization;

[Authorize(Policy = "FS13")]
public class StudentController : CrudController<Student, StudentDTO>
{
    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger, IStudentService service) : base(service)
    {
        _logger = logger;
    }
}