namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.UpdateTeacher;

public sealed class UpdateTeacherHandler(IUnitOfWork unitOfWork) : IHandler<UpdateTeacherCommand, TeacherDto?>
{
    public async Task<TeacherDto?> Handle(UpdateTeacherCommand command, CancellationToken cancellationToken)
    {
        var teacher = await unitOfWork.Teachers.GetByIdAsync(command.Id, cancellationToken);
        if (teacher is null) return null;

        var request = command.Request;
        if (await unitOfWork.Teachers.EmployeeNumberExistsAsync(request.EmployeeNumber, command.Id, cancellationToken))
            throw new InvalidOperationException("That employee number is already in use.");
        if (await unitOfWork.Teachers.EmailExistsAsync(request.Email, command.Id, cancellationToken))
            throw new InvalidOperationException("A teacher with that e-mail already exists.");

        teacher.UpdateDetails(request.EmployeeNumber, request.FirstName, request.LastName,
            request.Email, request.Department);
        unitOfWork.Teachers.Update(teacher);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new TeacherDto(teacher.Id, teacher.EmployeeNumber, teacher.FirstName, teacher.LastName,
            teacher.FullName, teacher.Email, teacher.Department, teacher.IsActive, teacher.CreatedAtUtc);
    }
}
