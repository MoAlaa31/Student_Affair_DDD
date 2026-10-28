namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.AddStudentsToClassroom;

public sealed record AddStudentsToClassroomCommand(int ClassroomId, IReadOnlyList<int> StudentIds) : IRequest<ClassroomDto?>;
