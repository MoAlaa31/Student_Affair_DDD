namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.CreateClassroom;

public sealed class CreateClassroomHandler(IUnitOfWork unitOfWork) : IHandler<CreateClassroomCommand, ClassroomDto>
{
    public async Task<ClassroomDto> Handle(CreateClassroomCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        if (await unitOfWork.Classrooms.CodeExistsAsync(request.Code, cancellationToken: cancellationToken))
            throw new InvalidOperationException("A classroom with that code already exists.");

        var classroom = new Classroom
        {
            Code = request.Code.Trim().ToUpperInvariant(),
            Building = request.Building.Trim(),
            RoomNumber = request.RoomNumber.Trim(),
            Capacity = request.Capacity,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };

        await unitOfWork.Classrooms.AddAsync(classroom, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ToDto(classroom);
    }

    private static ClassroomDto ToDto(Classroom classroom) => new(
        classroom.Id, classroom.Code, classroom.Building, classroom.RoomNumber,
        classroom.Capacity, classroom.IsActive, classroom.CreatedAtUtc);
}
