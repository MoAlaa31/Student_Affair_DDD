namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.DeleteClassroom;

public sealed record DeleteClassroomCommand(int Id) : IRequest<bool>;
