namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.DeleteTeacher;

public sealed class DeleteTeacherHandler(IUnitOfWork unitOfWork) : IHandler<DeleteTeacherCommand, bool>
{
    public async Task<bool> Handle(DeleteTeacherCommand command, CancellationToken cancellationToken)
    {
        var teacher = await unitOfWork.Teachers.GetByIdAsync(command.Id, cancellationToken);
        if (teacher is null) return false;
        unitOfWork.Teachers.Remove(teacher);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
