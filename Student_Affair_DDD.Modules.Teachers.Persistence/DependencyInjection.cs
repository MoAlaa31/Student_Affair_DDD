using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Student_Affair_DDD.Modules.Teachers.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddTeachersPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Teachers")
            ?? throw new InvalidOperationException("Connection string 'Teachers' is required.");

        services.AddDbContext<TeachersDbContext>(options =>
            options.UseSqlServer(connection, sql => sql.EnableRetryOnFailure()));
        services.AddScoped<ITeacherRepository, TeachersRepository>();
        services.AddScoped<IUnitOfWork, TeachersUnitOfWork>();
        return services;
    }
}
