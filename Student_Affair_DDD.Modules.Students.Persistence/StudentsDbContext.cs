
namespace Student_Affair_DDD.Modules.Students.Persistence;

public sealed class StudentsDbContext(DbContextOptions<StudentsDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var student = modelBuilder.Entity<Student>();
        student.ToTable("Students");
        student.HasKey(x => x.Id);
        student.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        student.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        student.Property(x => x.StudentNumber).HasMaxLength(20).IsRequired();
        student.HasIndex(x => x.StudentNumber).IsUnique();
        student.Property(x => x.NationalId).HasMaxLength(20).IsRequired();
        student.HasIndex(x => x.NationalId).IsUnique();
        student.Property(x => x.Email).HasMaxLength(200).IsRequired();
        student.HasIndex(x => x.Email).IsUnique();
        student.Property(x => x.Department).HasMaxLength(100).IsRequired();
        student.Property(x => x.Gpa).HasPrecision(3, 2);
        student.Ignore(x => x.FullName);
    }
}
