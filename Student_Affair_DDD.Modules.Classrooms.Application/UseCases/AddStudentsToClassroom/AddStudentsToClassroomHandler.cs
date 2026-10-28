namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.AddStudentsToClassroom;

public sealed class AddStudentsToClassroomHandler(IUnitOfWork unitOfWork, IStudentDirectory studentDirectory)
    : IHandler<AddStudentsToClassroomCommand, ClassroomDto?>
{
    public async Task<ClassroomDto?> Handle(AddStudentsToClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.ClassroomId, cancellationToken);
        if (classroom is null) return null;

        var requestedStudentIds = command.StudentIds ?? [];
        var studentIds = requestedStudentIds.Distinct().ToArray();
        if (studentIds.Length == 0 || studentIds.Length != requestedStudentIds.Count)
            throw new InvalidOperationException("Provide one or more unique student IDs.");
        if (studentIds.Any(id => classroom.Students.Any(existing => existing.StudentId == id)))
            throw new InvalidOperationException("One or more students are already assigned to this classroom.");
        if (classroom.Students.Count + studentIds.Length > classroom.Capacity)
            throw new InvalidOperationException("The number of students cannot exceed classroom capacity.");

        var unavailable = await studentDirectory.GetUnavailableStudentIdsAsync(studentIds, cancellationToken);
        if (unavailable.Count > 0)
            throw new InvalidOperationException($"These students do not exist or are inactive: {string.Join(", ", unavailable)}.");

        foreach (var studentId in studentIds)
            classroom.Students.Add(new ClassroomStudent { ClassroomId = classroom.Id, StudentId = studentId });

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ToDto(classroom);
    }

    private static ClassroomDto ToDto(Classroom classroom) => new(classroom.Id, classroom.Code,
        classroom.Building, classroom.RoomNumber, classroom.Capacity, classroom.IsActive,
        classroom.CreatedAtUtc, classroom.Students.Select(x => x.StudentId).ToArray(),
        classroom.Teachers.Select(x => x.TeacherId).ToArray());
}
