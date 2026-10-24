using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Student_Affair_DDD.Modules.Teachers.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Teachers.Application.Common.Interfaces.EntitiesIRepos;

namespace Student_Affair_DDD.Modules.Teachers.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddTeachersPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Teachers")
            ?? throw new InvalidOperationException("Connection string 'Teachers' is required.");

        services.AddDbContext<TeachersDbContext>(options => options.UseNpgsql(connection));
        services.AddScoped<ITeacherRepository, TeachersRepository>();
        services.AddScoped<IUnitOfWork, TeachersUnitOfWork>();
        return services;
    }
}
