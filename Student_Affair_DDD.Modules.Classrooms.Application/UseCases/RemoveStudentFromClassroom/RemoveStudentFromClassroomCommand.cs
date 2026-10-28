namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.RemoveStudentFromClassroom;

public sealed record RemoveStudentFromClassroomCommand(int ClassroomId, int StudentId) : IRequest<bool>;
