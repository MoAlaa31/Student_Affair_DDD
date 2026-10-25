using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Student_Affair_DDD.Modules.Classrooms.Application.Classrooms;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces.EntitiesIRepos;

namespace Student_Affair_DDD.Modules.Classrooms.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddClassroomsPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Classrooms")
                ?? throw new InvalidOperationException("Connection string 'Classrooms' is required.");

            services.AddDbContext<ClassroomsDbContext>(options =>
                options.UseSqlServer(connection, sql => sql.EnableRetryOnFailure()));
            services.AddScoped<IClassroomRepository, ClassroomsRepository>();
            services.AddScoped<IUnitOfWork, ClassroomsUnitOfWork>();
            return services;
        }
    }
}
