namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.GetTeacherById;

public sealed record GetTeacherByIdQuery(int Id) : IRequest<TeacherDto?>;
