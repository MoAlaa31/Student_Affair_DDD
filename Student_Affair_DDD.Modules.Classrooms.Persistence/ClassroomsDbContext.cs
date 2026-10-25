using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Student_Affair_DDD.Modules.Classrooms.Persistence;

public sealed class ClassroomsDbContext(DbContextOptions<ClassroomsDbContext> options) : DbContext(options)
{
    public DbSet<Classroom> Classrooms => Set<Classroom>();

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
    }
}

