
namespace Student_Affair_DDD.Modules.Teachers.Persistence;

public sealed class TeachersDbContext(DbContextOptions<TeachersDbContext> options) : DbContext(options)
{
    public DbSet<Teacher> Teachers => Set<Teacher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var teacher = modelBuilder.Entity<Teacher>();
        teacher.ToTable("Teachers");
        teacher.HasKey(x => x.Id);
        teacher.Property(x => x.EmployeeNumber).HasMaxLength(20).IsRequired();
        teacher.HasIndex(x => x.EmployeeNumber).IsUnique();
        teacher.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        teacher.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        teacher.Property(x => x.Email).HasMaxLength(200).IsRequired();
        teacher.HasIndex(x => x.Email).IsUnique();
        teacher.Property(x => x.Department).HasMaxLength(100).IsRequired();
        teacher.Ignore(x => x.FullName);
    }
}

