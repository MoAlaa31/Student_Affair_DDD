namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.GetTeacherById;

public sealed class GetTeacherByIdHandler(ITeacherRepository repository)
    : IHandler<GetTeacherByIdQuery, TeacherDto?>
{
    public async Task<TeacherDto?> Handle(GetTeacherByIdQuery query, CancellationToken cancellationToken)
    {
        var teacher = await repository.GetByIdAsync(query.Id, cancellationToken);
        return teacher is null ? null : new TeacherDto(teacher.Id, teacher.EmployeeNumber,
            teacher.FirstName, teacher.LastName, teacher.FullName, teacher.Email, teacher.Department,
            teacher.IsActive, teacher.CreatedAtUtc);
    }
}
