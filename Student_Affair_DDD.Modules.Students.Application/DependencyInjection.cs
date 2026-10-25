using Microsoft.Extensions.DependencyInjection;
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Students.Application.Services;

namespace Student_Affair_DDD.Modules.Students.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentsApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();
        var assembly = typeof(DependencyInjection).Assembly;
        foreach (var implementation in assembly.GetTypes().Where(t => t is { IsAbstract: false, IsInterface: false }))
        {
            foreach (var contract in implementation.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>)))
                services.AddScoped(contract, implementation);
        }

        services.AddScoped<IStudentService, StudentServiceDispatcherAdapter>();
        return services;
    }
}
