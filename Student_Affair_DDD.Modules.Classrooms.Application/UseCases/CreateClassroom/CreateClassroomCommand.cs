namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.CreateClassroom;

public sealed record CreateClassroomCommand(ClassroomCreateDto Request) : IRequest<ClassroomDto>;
