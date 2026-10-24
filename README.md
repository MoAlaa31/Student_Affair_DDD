# Student Affairs — DDD modular monolith

This is a separate starter solution; the existing `Student_Affair_Blazor` solution is unchanged.

## Shape

- `Student_Affair_DDD.Host` is the only executable and deployment unit. It composes modules and exposes HTTP endpoints.
- `Student_Affair_DDD.Modules.Students.Domain` owns the Student aggregate and its invariants.
- `Student_Affair_DDD.Modules.Students.Application` owns the Students use cases and request dispatcher.
- Each module owns its `Domain`, `Application`, `Persistence`, `Shared`, and `Web` layers. `Shared` contains client-facing DTOs and API abstractions; `Web` contains MVC controllers.
- `Student_Affair_DDD.Web.Client` is the single Blazor WebAssembly frontend. It references each module's `Shared` project and provides one shared layout with Students at `/` (also `/students`), Classrooms at `/classrooms`, and Teachers at `/teachers`.
- `Student_Affair_DDD.Contracts` contains API request/response contracts, not domain entities.

The Host calls each module's registration methods. Modules should not access each other's DbContexts or tables; add explicit contracts or module events when cross-domain collaboration is needed. Classroom and Teacher are separate modules and currently have no direct relationship. If a future workflow assigns teachers to classrooms, put that assignment in a dedicated scheduling/teaching module, or introduce an explicit module contract. Deploy the Host once; it contains the module assemblies.

## Start

1. Configure `ConnectionStrings:Students`, `ConnectionStrings:Classrooms`, and `ConnectionStrings:Teachers` in `Student_Affair_DDD.Host/appsettings.json` or user secrets. Students uses SQL Server, Classrooms uses Oracle, and Teachers uses PostgreSQL. Update the sample Oracle service/user and PostgreSQL database/user values for your local installations; do not commit real passwords.
2. Create and apply migrations for the two new providers from the repository root (install `dotnet-ef` if needed):

   ```powershell
   dotnet ef migrations add InitialClassrooms --project Student_Affair_DDD.Modules.Classrooms.Persistence --startup-project Student_Affair_DDD.Host --context ClassroomsDbContext --output-dir Migrations
   dotnet ef database update --project Student_Affair_DDD.Modules.Classrooms.Persistence --startup-project Student_Affair_DDD.Host --context ClassroomsDbContext
   dotnet ef migrations add InitialTeachers --project Student_Affair_DDD.Modules.Teachers.Persistence --startup-project Student_Affair_DDD.Host --context TeachersDbContext --output-dir Migrations
   dotnet ef database update --project Student_Affair_DDD.Modules.Teachers.Persistence --startup-project Student_Affair_DDD.Host --context TeachersDbContext
   ```

   Run the same migration workflow for Students only if its schema needs to be created or updated; its SQL Server provider is unchanged.
3. Run `dotnet run --project Student_Affair_DDD.Host` from this folder. The Host serves the single Blazor client at `/`; navigate between the Students, Classrooms, and Teachers screens without reloading another client. In Development, OpenAPI is available at `/openapi/v1.json`.

Endpoints: `GET /health`; student CRUD and paging routes under `/api/students`; and `GET` and `POST` routes under `/api/classrooms` and `/api/teachers`.

Each module owns its repository and unit of work; the Host composes active modules and serves their client assets.
