namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.RemoveTeacherFromClassroom;

public sealed record RemoveTeacherFromClassroomCommand(int ClassroomId, int TeacherId) : IRequest<bool>;
