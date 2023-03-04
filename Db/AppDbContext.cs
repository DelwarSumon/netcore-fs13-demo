using System.Reflection;
namespace NETCoreDemo.Db;

using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Models;
using Npgsql;

public class AppDbContext : DbContext
{
    // Static constructor which will be run ONCE
    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Course.CourseStatus>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ProjectRole>();
        // You can also do that automatically using Reflection

        // Use the legacy timestamp behaviour - check Npgsql for more details
        // Recommendation from Postgres: Don't use time zone in database
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private readonly IConfiguration _config;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options) => _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = _config.GetConnectionString("DefaultConnection");
        optionsBuilder
            .UseNpgsql(connString)
            .AddInterceptors(new AppDbContextSaveChangesInterceptor())
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map C# enum to Postgres enum
        modelBuilder.HasPostgresEnum<Course.CourseStatus>();
        modelBuilder.HasPostgresEnum<ProjectRole>();

        // Create an index on Name using Fluent API
        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Name);

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)
            .IsUnique();

        // Tell EF Core to use a composite primary key
        modelBuilder.Entity<ProjectStudent>()
            .HasKey(ps => new { ps.ProjectId, ps.StudentId });

        // TODO: Do this in a better way using loop
        modelBuilder.Entity<Student>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Student>()
            .Property(s => s.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Address>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Address>()
            .Property(s => s.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Address)
            .WithOne()
            .OnDelete(DeleteBehavior.SetNull);

        // Always load Address along with the Student
        // modelBuilder.Entity<Student>().Navigation(s => s.Address).AutoInclude();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<ProjectStudent> ProjectStudents { get; set; } = null!;
}
