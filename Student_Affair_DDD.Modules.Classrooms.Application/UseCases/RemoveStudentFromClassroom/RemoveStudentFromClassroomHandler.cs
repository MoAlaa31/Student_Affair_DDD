namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.RemoveStudentFromClassroom;

public sealed class RemoveStudentFromClassroomHandler(IUnitOfWork unitOfWork)
    : IHandler<RemoveStudentFromClassroomCommand, bool>
{
    public async Task<bool> Handle(RemoveStudentFromClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.ClassroomId, cancellationToken);
        if (classroom is null) return false;
        var assignment = classroom.Students.FirstOrDefault(x => x.StudentId == command.StudentId);
        if (assignment is null) return false;
        if (classroom.Students.Count <= 1)
            throw new InvalidOperationException("A classroom must have at least one student assigned.");

        classroom.Students.Remove(assignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
