namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.SetClassroomActive;

public sealed record SetClassroomActiveCommand(int Id, bool IsActive) : IRequest<bool>;
