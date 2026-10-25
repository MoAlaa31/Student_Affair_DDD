using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Student_Affair_DDD.Modules.Students.Persistence;

public static class DependencyInjection
{
    public const string ConnectionStringName = "Students";
    public static IServiceCollection AddStudentsPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
            ?? throw new InvalidOperationException("Connection string 'Students' is required.");

        services.AddDbContext<StudentsDbContext>(options => 
            options.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly(typeof(StudentsDbContext).Assembly.FullName);
                sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            })
        );
        services.AddScoped<IStudentRepository, StudentsRepository>();
        services.AddScoped<IUnitOfWork, StudentsUnitOfWork>();
        return services;
    }
}
