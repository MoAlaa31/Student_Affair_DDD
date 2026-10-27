namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.UpdateClassroom;

public sealed record UpdateClassroomCommand(int Id, ClassroomUpdateDto Request) : IRequest<ClassroomDto?>;
