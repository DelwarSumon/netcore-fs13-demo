namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

public class ProjectController : CrudController<Project, ProjectDTO>
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IProjectService _projetService;

    public ProjectController(ILogger<ProjectController> logger, IProjectService service) : base(service)
    {
        _logger = logger;
        _projetService = service;
    }

    // GET /projects/{id}/students
    // POST /projects/{id}/students - create students for project
    // DELETE /projects/{id}/students - remove students from project

    // POST /projects/{id}/add-students - create students for project
    // POST /projects/{id}/remove-students - remove students from project

    [HttpPost("{id}/add-students")]
    public async Task<IActionResult> AddStudents(int id, ICollection<ProjectAddStudentsDTO> request)
    {
        var added = await _projetService.AddStudentsAsync(id, request);
        if (added <= 0)
        {
            return BadRequest("No valid student found");
        }
        return Ok(new { Count = added });
    }
}