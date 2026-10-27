namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.CheckClassroomCodeAvailability;

public sealed class CheckClassroomCodeAvailabilityHandler(IClassroomRepository repository)
    : IHandler<CheckClassroomCodeAvailabilityQuery, bool>
{
    public async Task<bool> Handle(CheckClassroomCodeAvailabilityQuery query, CancellationToken cancellationToken)
        => !await repository.CodeExistsAsync(query.Code, query.ExcludeId, cancellationToken);
}
