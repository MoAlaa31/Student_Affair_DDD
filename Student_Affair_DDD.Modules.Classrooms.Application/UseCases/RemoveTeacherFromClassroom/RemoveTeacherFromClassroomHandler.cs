namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.RemoveTeacherFromClassroom;

public sealed class RemoveTeacherFromClassroomHandler(IUnitOfWork unitOfWork)
    : IHandler<RemoveTeacherFromClassroomCommand, bool>
{
    public async Task<bool> Handle(RemoveTeacherFromClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.ClassroomId, cancellationToken);
        if (classroom is null) return false;
        var assignment = classroom.Teachers.FirstOrDefault(x => x.TeacherId == command.TeacherId);
        if (assignment is null) return false;
        if (classroom.Teachers.Count <= 1)
            throw new InvalidOperationException("A classroom must have at least one teacher assigned.");

        classroom.Teachers.Remove(assignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
