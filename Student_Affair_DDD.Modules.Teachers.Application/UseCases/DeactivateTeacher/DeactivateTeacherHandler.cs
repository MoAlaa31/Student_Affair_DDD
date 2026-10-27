namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.DeactivateTeacher;

public sealed class DeactivateTeacherHandler(IUnitOfWork unitOfWork) : IHandler<DeactivateTeacherCommand, bool>
{
    public async Task<bool> Handle(DeactivateTeacherCommand command, CancellationToken cancellationToken)
    {
        var teacher = await unitOfWork.Teachers.GetByIdAsync(command.Id, cancellationToken);
        if (teacher is null) return false;
        teacher.Deactivate();
        unitOfWork.Teachers.Update(teacher);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
