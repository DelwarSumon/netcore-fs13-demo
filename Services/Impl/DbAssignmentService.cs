namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Db;

public class DbAssignmentService : DbCrudService<Assignment, AssignmentDTO>, IAssignmentService
{
    public DbAssignmentService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> AssignStudentsAsync(int id, ICollection<int> studentIds)
    {
        var assignment = await GetAsync(id);
        if (assignment is null)
        {
            return -1;
        }

        var students = await _dbContext.Students
            .Include(s => s.Assignments) // Eager loading
            .Where(s => studentIds.Contains(s.Id))
            .ToListAsync(); // 1 query

        // Eager loading: 1 query
        // Explicit loading: 1 + sizeof(students)
        // DoS: Denial of Service

        foreach (var student in students)
        {
            if (!student.Assignments.Contains(assignment))
            {
                student.Assignments.Add(assignment);
            }
        }
        await _dbContext.SaveChangesAsync();

        return students.Count();
    }

    // TODO: Implement this and also the endpoint
    public Task<int> RemoveStudentsAsync(int id, ICollection<int> students)
    {
        throw new NotImplementedException();
    }
}