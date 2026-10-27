namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.CheckTeacherEmployeeNumberAvailability;

public sealed class CheckTeacherEmployeeNumberAvailabilityHandler(ITeacherRepository repository)
    : IHandler<CheckTeacherEmployeeNumberAvailabilityQuery, bool>
{
    public async Task<bool> Handle(CheckTeacherEmployeeNumberAvailabilityQuery query, CancellationToken cancellationToken)
        => !await repository.EmployeeNumberExistsAsync(query.EmployeeNumber, query.ExcludeId, cancellationToken);
}
