namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.GetClassroomById;

public sealed class GetClassroomByIdHandler(IClassroomRepository repository)
    : IHandler<GetClassroomByIdQuery, ClassroomDto?>
{
    public async Task<ClassroomDto?> Handle(GetClassroomByIdQuery query, CancellationToken cancellationToken)
    {
        var classroom = await repository.GetByIdAsync(query.Id, cancellationToken);
        return classroom is null ? null : new ClassroomDto(classroom.Id, classroom.Code,
            classroom.Building, classroom.RoomNumber, classroom.Capacity, classroom.IsActive,
            classroom.CreatedAtUtc);
    }
}
