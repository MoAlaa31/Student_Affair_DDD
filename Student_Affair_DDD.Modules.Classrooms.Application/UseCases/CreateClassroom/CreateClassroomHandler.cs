namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.CreateClassroom;

public sealed class CreateClassroomHandler(
    IUnitOfWork unitOfWork,
    IStudentDirectory studentDirectory,
    ITeacherDirectory teacherDirectory) : IHandler<CreateClassroomCommand, ClassroomDto>
{
    public async Task<ClassroomDto> Handle(CreateClassroomCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        if (await unitOfWork.Classrooms.CodeExistsAsync(request.Code, cancellationToken: cancellationToken))
            throw new InvalidOperationException("A classroom with that code already exists.");

        var requestedStudentIds = request.StudentIds ?? [];
        var requestedTeacherIds = request.TeacherIds ?? [];
        var studentIds = requestedStudentIds.Distinct().ToArray();
        var teacherIds = requestedTeacherIds.Distinct().ToArray();
        if (studentIds.Length == 0 || teacherIds.Length == 0)
            throw new InvalidOperationException("A classroom must have at least one student and one teacher.");
        if (studentIds.Length != requestedStudentIds.Length || teacherIds.Length != requestedTeacherIds.Length)
            throw new InvalidOperationException("Duplicate student or teacher IDs are not allowed.");
        if (studentIds.Length > request.Capacity)
            throw new InvalidOperationException("The number of students cannot exceed classroom capacity.");

        var unavailableStudents = await studentDirectory.GetUnavailableStudentIdsAsync(studentIds, cancellationToken);
        if (unavailableStudents.Count > 0)
            throw new InvalidOperationException($"These students do not exist or are inactive: {string.Join(", ", unavailableStudents)}.");

        var unavailableTeachers = await teacherDirectory.GetUnavailableTeacherIdsAsync(teacherIds, cancellationToken);
        if (unavailableTeachers.Count > 0)
            throw new InvalidOperationException($"These teachers do not exist or are inactive: {string.Join(", ", unavailableTeachers)}.");

        var classroom = new Classroom
        {
            Code = request.Code.Trim().ToUpperInvariant(),
            Building = request.Building.Trim(),
            RoomNumber = request.RoomNumber.Trim(),
            Capacity = request.Capacity,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow,
            Students = studentIds.Select(id => new ClassroomStudent { StudentId = id }).ToList(),
            Teachers = teacherIds.Select(id => new ClassroomTeacher { TeacherId = id }).ToList()
        };

        await unitOfWork.Classrooms.AddAsync(classroom, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ToDto(classroom);
    }

    private static ClassroomDto ToDto(Classroom classroom) => new(
        classroom.Id, classroom.Code, classroom.Building, classroom.RoomNumber,
        classroom.Capacity, classroom.IsActive, classroom.CreatedAtUtc,
        classroom.Students.Select(x => x.StudentId).ToArray(),
        classroom.Teachers.Select(x => x.TeacherId).ToArray());
}
