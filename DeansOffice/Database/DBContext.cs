using DeansOffice.Database.Models;
using DeansOffice.Database;
using Microsoft.EntityFrameworkCore;

public class DeanDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<GroupSubject> GroupSubjects { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=dean.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeacherSubject>()
            .HasKey(ts => new { ts.TeacherID, ts.SubjectID });

        modelBuilder.Entity<GroupSubject>()
            .HasKey(gs => new { gs.GroupID, gs.SubjectID });

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Curator)
            .WithMany(t => t.CuratedGroups)
            .HasForeignKey(g => g.CuratorID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}