using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using NETCoreDemo.Db;
using Microsoft.EntityFrameworkCore;

namespace NETCoreDemo.Services;

public class DbStudentService : DbCrudService<Student, StudentDTO>, IStudentService
{
    public DbStudentService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<ICollection<Student>> GetAllAsync()
    {
        return await _dbContext.Students
            .AsNoTracking()
            // Eager loading
            .Include(s => s.Address)
            .Include(s => s.Course)
            .Include(s => s.Assignments)
            .Include(s => s.ProjectLinks) // => a list of ProjectStudent
                .ThenInclude(ps => ps.Project)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public override async Task<Student?> GetAsync(int id)
    {
        var student = await base.GetAsync(id);
        if (student is null)
        {
            return null;
        }
        // Explicit loading
        await _dbContext.Entry(student).Reference(s => s.Address).LoadAsync();
        await _dbContext.Entry(student).Reference(s => s.Course).LoadAsync();
        return student;
    }

    public ICollection<Student> GetStudentsWithJobs()
    {
        throw new NotImplementedException();
    }

    public ICollection<Student> GetTopStudents()
    {
        throw new NotImplementedException();
    }
}