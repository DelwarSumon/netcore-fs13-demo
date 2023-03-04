using System.Runtime.CompilerServices;
namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using NETCoreDemo.Db;
using Microsoft.EntityFrameworkCore;

public class DbProjectService : DbCrudService<Project, ProjectDTO>, IProjectService
{
    public DbProjectService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Project?> GetAsync(int id)
    {
        return await _dbContext.Projects
            .Include(p => p.StudentLinks)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> AddStudentsAsync(int id, ICollection<ProjectAddStudentsDTO> request)
    {
        var project = await GetAsync(id);
        if (project is null)
        {
            return -1;
        }
        var studentIds = request
            .Select(item => item.StudentId)
            .ToList();

        // [-1, 1, 2]
        // Select * from students where id in (-1, 1, 2);

        var students = await _dbContext.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToListAsync();

        foreach (var student in students)
        {
            /*
            project.StudentLinks.Add(new ProjectStudent
            {
                Project = project,
                JoinedAt = DateTime.Now,
                // TODO: We need to get the Role, how?
                // Role = item.Role,
                Student = student,
            });*/
        }

        foreach (var item in request)
        {
            project.StudentLinks.Add(new ProjectStudent
            {
                Project = project,
                JoinedAt = DateTime.Now,
                Role = item.Role,
                StudentId = item.StudentId,
            });
        }
        await _dbContext.SaveChangesAsync();
        return students.Count();
    }
}