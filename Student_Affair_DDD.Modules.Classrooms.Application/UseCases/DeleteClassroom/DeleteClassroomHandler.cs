namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.DeleteClassroom;

public sealed class DeleteClassroomHandler(IUnitOfWork unitOfWork) : IHandler<DeleteClassroomCommand, bool>
{
    public async Task<bool> Handle(DeleteClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.Id, cancellationToken);
        if (classroom is null) return false;
        unitOfWork.Classrooms.Remove(classroom);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
