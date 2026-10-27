namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.UpdateTeacher;

public sealed record UpdateTeacherCommand(int Id, TeacherUpdateDto Request) : IRequest<TeacherDto?>;
