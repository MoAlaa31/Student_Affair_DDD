namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.DeactivateTeacher;

public sealed record DeactivateTeacherCommand(int Id) : IRequest<bool>;
