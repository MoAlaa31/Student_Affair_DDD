using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Student_Affair_DDD.Modules.Classrooms.Persistence;

public sealed class ClassroomsDbContext(DbContextOptions<ClassroomsDbContext> options) : DbContext(options)
{
    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<ClassroomStudent> ClassroomStudents => Set<ClassroomStudent>();
    public DbSet<ClassroomTeacher> ClassroomTeachers => Set<ClassroomTeacher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var classroom = modelBuilder.Entity<Classroom>();
        classroom.ToTable("Classrooms");
        classroom.HasKey(x => x.Id);
        classroom.Property(x => x.Code).HasMaxLength(20).IsRequired();
        classroom.HasIndex(x => x.Code).IsUnique();
        classroom.Property(x => x.Building).HasMaxLength(100).IsRequired();
        classroom.Property(x => x.RoomNumber).HasMaxLength(20).IsRequired();
        classroom.Property(x => x.Capacity).IsRequired();

        var students = modelBuilder.Entity<ClassroomStudent>();
        students.ToTable("ClassroomStudents");
        students.HasKey(x => new { x.ClassroomId, x.StudentId });
        students.HasOne<Classroom>()
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.ClassroomId)
            .OnDelete(DeleteBehavior.Cascade);

        var teachers = modelBuilder.Entity<ClassroomTeacher>();
        teachers.ToTable("ClassroomTeachers");
        teachers.HasKey(x => new { x.ClassroomId, x.TeacherId });
        teachers.HasOne<Classroom>()
            .WithMany(x => x.Teachers)
            .HasForeignKey(x => x.ClassroomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

