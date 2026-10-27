namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.GetClassroomById;

public sealed record GetClassroomByIdQuery(int Id) : IRequest<ClassroomDto?>;
