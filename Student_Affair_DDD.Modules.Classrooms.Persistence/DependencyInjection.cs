using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Classrooms.Persistence.Integrations;

namespace Student_Affair_DDD.Modules.Classrooms.Persistence
{
    public static class DependencyInjection
    {
        public const string ConnectionStringName = "Classrooms";
        public static IServiceCollection AddClassroomsPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString(ConnectionStringName)
                ?? throw new InvalidOperationException("Connection string 'Classrooms' is required.");

            services.AddDbContext<ClassroomsDbContext>(options =>
                options.UseNpgsql(connection, postgres => postgres.EnableRetryOnFailure()));
            services.AddHttpClient<IStudentDirectory, StudentsApiDirectory>(client =>
                client.BaseAddress = new Uri(configuration["ModuleApiBaseUrls:Students"] ?? "http://localhost:5239/"));
            services.AddHttpClient<ITeacherDirectory, TeachersApiDirectory>(client =>
                client.BaseAddress = new Uri(configuration["ModuleApiBaseUrls:Teachers"] ?? "http://localhost:5239/"));
            services.AddScoped<IClassroomRepository, ClassroomsRepository>();
            services.AddScoped<IUnitOfWork, ClassroomsUnitOfWork>();
            return services;
        }
    }
}
