namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

public class AssignmentController : CrudController<Assignment, AssignmentDTO>
{
    private readonly ILogger<AssignmentController> _logger;

    private readonly IAssignmentService _assignmentService;

    public AssignmentController(ILogger<AssignmentController> logger,
        IAssignmentService service) : base(service)
    {
        _logger = logger;
        _assignmentService = service;
    }

    [HttpPost("{id}/assign")]
    public async Task<IActionResult> AssignStudents(int id, AssignStudentsDTO request)
    {
        var assigned = await _assignmentService.AssignStudentsAsync(id, request.Students);
        if (assigned <= 0)
        {
            return BadRequest("Wrong student Ids");
        }
        return Ok(new { Count = assigned });
    }
}