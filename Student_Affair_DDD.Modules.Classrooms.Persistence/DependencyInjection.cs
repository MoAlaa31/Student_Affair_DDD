using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IClassroomRepository, ClassroomsRepository>();
            services.AddScoped<IUnitOfWork, ClassroomsUnitOfWork>();
            return services;
        }
    }
}
