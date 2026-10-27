namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.DeleteTeacher;

public sealed record DeleteTeacherCommand(int Id) : IRequest<bool>;
