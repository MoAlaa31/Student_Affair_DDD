namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.AddTeachersToClassroom;

public sealed record AddTeachersToClassroomCommand(int ClassroomId, IReadOnlyList<int> TeacherIds) : IRequest<ClassroomDto?>;
