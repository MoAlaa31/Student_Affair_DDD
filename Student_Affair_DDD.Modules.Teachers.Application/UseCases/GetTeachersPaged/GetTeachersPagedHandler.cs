namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.GetTeachersPaged;

public sealed class GetTeachersPagedHandler(ITeacherRepository repository)
    : IHandler<GetTeachersPagedQuery, PagedTeachers>
{
    public async Task<PagedTeachers> Handle(GetTeachersPagedQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await repository.GetPagedAsync(query.Spec, cancellationToken);
        var results = items.Select(teacher => new TeacherDto(teacher.Id, teacher.EmployeeNumber,
            teacher.FirstName, teacher.LastName, teacher.FullName, teacher.Email, teacher.Department,
            teacher.IsActive, teacher.CreatedAtUtc)).ToArray();
        return new PagedTeachers(results, totalCount, query.Spec.PageIndex, query.Spec.PageSize);
    }
}
