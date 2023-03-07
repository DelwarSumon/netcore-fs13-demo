using NETCoreDemo.Db;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace NETCoreDemo.Services;

public class DbCourseSerivce : DbCrudService<Course, CourseDTO>, ICourseService
{
    public DbCourseSerivce(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<ICollection<Course>> GetAllAsync(ICrudFilter? filter)
    {
        var courseFilter = (CourseFilterDTO?)filter;
        var query = _dbContext.Courses.Where(c => true);

        if (courseFilter?.Status is not null)
        {
            query = query.Where(c => c.Status == courseFilter.Status);
        }
        if (courseFilter?.Keyword is not null && !string.IsNullOrEmpty(courseFilter?.Keyword))
        {
            query = query.Where(c => c.Name.Contains(courseFilter!.Keyword));
        }
        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public override async Task<Course?> GetAsync(int id)
    {
        var course = await base.GetAsync(id);
        if (course is null)
        {
            return null;
        }
        // Explicit loading
        await _dbContext.Entry(course).Collection(c => c.Students).LoadAsync();
        return course;
    }

    public async Task<ICollection<Course>> GetCoursesByStatusAsync(Course.CourseStatus status)
    {
        return await _dbContext.Courses
            .Where(c => c.Status == status)
            .ToListAsync();
    }
}