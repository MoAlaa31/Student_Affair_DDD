namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.SetClassroomActive;

public sealed class SetClassroomActiveHandler(IUnitOfWork unitOfWork) : IHandler<SetClassroomActiveCommand, bool>
{
    public async Task<bool> Handle(SetClassroomActiveCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.Id, cancellationToken);
        if (classroom is null) return false;
        classroom.IsActive = command.IsActive;
        unitOfWork.Classrooms.Update(classroom);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
