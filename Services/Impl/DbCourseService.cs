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

    public override async Task<ICollection<Course>> GetAllAsync()
    {
        Func<Course, Course> process = course =>
        {
            course.StudentCount = course.Students.Count();
            return course;
        };
        var items = await _dbContext.Courses
            .Select(c => new
                {
                    Course = c,
                    StudentCount = c.Students.Count(),
                    LatestStudents = c.Students.OrderByDescending(s => s.CreatedAt).Take(5),
                }
            )
            .Where(c => c.StudentCount >= 1)
            .ToListAsync();
        foreach (var item in items)
        {
            item.Course.StudentCount = item.StudentCount;
            item.Course.LatestStudents = item.LatestStudents.ToList();
        };
        return items.Select(item => item.Course).ToList();
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