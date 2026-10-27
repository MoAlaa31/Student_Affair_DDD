namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.CreateTeacher;

public sealed class CreateTeacherHandler(IUnitOfWork unitOfWork) : IHandler<CreateTeacherCommand, TeacherDto>
{
    public async Task<TeacherDto> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        if (await unitOfWork.Teachers.EmployeeNumberExistsAsync(request.EmployeeNumber, cancellationToken: cancellationToken))
            throw new InvalidOperationException("That employee number is already in use.");
        if (await unitOfWork.Teachers.EmailExistsAsync(request.Email, cancellationToken: cancellationToken))
            throw new InvalidOperationException("A teacher with that e-mail already exists.");

        var teacher = Teacher.Hire(request.EmployeeNumber, request.FirstName, request.LastName,
            request.Email, request.Department);
        await unitOfWork.Teachers.AddAsync(teacher, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ToDto(teacher);
    }

    private static TeacherDto ToDto(Teacher teacher) => new(teacher.Id, teacher.EmployeeNumber,
        teacher.FirstName, teacher.LastName, teacher.FullName, teacher.Email, teacher.Department,
        teacher.IsActive, teacher.CreatedAtUtc);
}
