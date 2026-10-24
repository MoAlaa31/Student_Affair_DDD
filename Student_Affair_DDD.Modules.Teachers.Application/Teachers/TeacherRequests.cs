using Student_Affair_DDD.Modules.Teachers.Application.Messaging;
using Student_Affair_DDD.Modules.Teachers.Domain;
using Student_Affair_DDD.Modules.Teachers.Application.Common;
using Student_Affair_DDD.Modules.Teachers.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Teachers.Application.Common.Interfaces.EntitiesIRepos;
using Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Teachers.Application.Teachers;

public sealed record RegisterTeacher(TeacherCreateDto Request) : IRequest<TeacherDto>;
public sealed record GetTeachers(TeacherPaginationSpecParams Spec) : IRequest<PagedTeachers>;

public sealed class RegisterTeacherHandler(IUnitOfWork unitOfWork) : IHandler<RegisterTeacher, TeacherDto>
{
    public async Task<TeacherDto> Handle(RegisterTeacher command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        if (await unitOfWork.Teachers.EmployeeNumberExistsAsync(request.EmployeeNumber, cancellationToken: cancellationToken))
            throw new InvalidOperationException("That employee number is already in use.");
        if (await unitOfWork.Teachers.EmailExistsAsync(request.Email, cancellationToken: cancellationToken))
            throw new InvalidOperationException("A teacher with that e-mail already exists.");
        var teacher = Teacher.Hire(request.EmployeeNumber, request.FirstName, request.LastName, request.Email, request.Department);
        await unitOfWork.Teachers.AddAsync(teacher, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Map(teacher);
    }

    private static TeacherDto Map(Teacher t) => new(t.Id, t.EmployeeNumber, t.FirstName, t.LastName, t.FullName, t.Email, t.Department, t.IsActive, t.CreatedAtUtc);
}

public sealed class GetTeachersHandler(ITeacherRepository repository) : IHandler<GetTeachers, PagedTeachers>
{
    public async Task<PagedTeachers> Handle(GetTeachers query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await repository.GetPagedAsync(query.Spec, cancellationToken);
        var responses = items.Select(t => new TeacherDto(t.Id, t.EmployeeNumber, t.FirstName, t.LastName, t.FullName, t.Email, t.Department, t.IsActive, t.CreatedAtUtc)).ToArray();
        return new PagedTeachers(responses, totalCount, query.Spec.PageIndex, query.Spec.PageSize);
    }
}
