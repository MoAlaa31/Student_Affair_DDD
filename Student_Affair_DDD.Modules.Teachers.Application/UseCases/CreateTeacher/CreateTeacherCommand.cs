namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.CreateTeacher;

public sealed record CreateTeacherCommand(TeacherCreateDto Request) : IRequest<TeacherDto>;
