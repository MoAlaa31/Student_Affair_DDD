namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.CheckTeacherEmailAvailability;

public sealed class CheckTeacherEmailAvailabilityHandler(ITeacherRepository repository)
    : IHandler<CheckTeacherEmailAvailabilityQuery, bool>
{
    public async Task<bool> Handle(CheckTeacherEmailAvailabilityQuery query, CancellationToken cancellationToken)
        => !await repository.EmailExistsAsync(query.Email, query.ExcludeId, cancellationToken);
}
