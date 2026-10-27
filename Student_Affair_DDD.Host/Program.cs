using Student_Affair_DDD.Modules.Students.Application;
using Student_Affair_DDD.Modules.Students.Persistence;
using Student_Affair_DDD.Modules.Students.Web;
using Student_Affair_DDD.Modules.Classrooms.Persistence;
using Student_Affair_DDD.Modules.Classrooms.Web;
using Student_Affair_DDD.Modules.Teachers.Application.Messaging;
using Student_Affair_DDD.Modules.Teachers.Persistence;
using Student_Affair_DDD.Modules.Teachers.Web;
using Student_Affair_DDD.Modules.Classrooms.Application;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddOpenApi();
        builder.Services.AddControllers()
            .AddApplicationPart(typeof(StudentsController).Assembly)
            .AddApplicationPart(typeof(ClassroomsController).Assembly)
            .AddApplicationPart(typeof(TeachersController).Assembly)
            ;

        builder.Services.AddStudentsApplication();
        builder.Services.AddStudentsPersistence(builder.Configuration);

        builder.Services.AddClassroomsApplication();
        builder.Services.AddClassroomsPersistence(builder.Configuration);

        builder.Services.AddTeachersApplication();
        builder.Services.AddTeachersPersistence(builder.Configuration);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
            app.UseWebAssemblyDebugging();

        app.UseHttpsRedirection();
        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
        app.MapGet("/health", () => Results.Ok(new { status = "ok" })).WithName("Health");
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
