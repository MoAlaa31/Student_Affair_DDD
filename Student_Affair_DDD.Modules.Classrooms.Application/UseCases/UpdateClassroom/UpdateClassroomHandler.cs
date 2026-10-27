namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.UpdateClassroom;

public sealed class UpdateClassroomHandler(IUnitOfWork unitOfWork) : IHandler<UpdateClassroomCommand, ClassroomDto?>
{
    public async Task<ClassroomDto?> Handle(UpdateClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.Id, cancellationToken);
        if (classroom is null) return null;

        var request = command.Request;
        if (await unitOfWork.Classrooms.CodeExistsAsync(request.Code, command.Id, cancellationToken))
            throw new InvalidOperationException("A classroom with that code already exists.");

        classroom.Code = request.Code.Trim().ToUpperInvariant();
        classroom.Building = request.Building.Trim();
        classroom.RoomNumber = request.RoomNumber.Trim();
        classroom.Capacity = request.Capacity;
        unitOfWork.Classrooms.Update(classroom);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new ClassroomDto(classroom.Id, classroom.Code, classroom.Building, classroom.RoomNumber,
            classroom.Capacity, classroom.IsActive, classroom.CreatedAtUtc);
    }
}
