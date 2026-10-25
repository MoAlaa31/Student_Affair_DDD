using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Student_Affair_DDD.Web.Client;
using Student_Affair_DDD.Web.Client.Services;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped<IStudentApi, StudentApi>();
        builder.Services.AddScoped<IClassroomApi, ClassroomApi>();
        builder.Services.AddScoped<ITeacherApi, TeacherApi>();

        await builder.Build().RunAsync();
    }
}
