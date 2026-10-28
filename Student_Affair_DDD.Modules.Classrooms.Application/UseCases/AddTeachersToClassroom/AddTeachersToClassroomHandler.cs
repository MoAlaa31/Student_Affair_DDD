namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.AddTeachersToClassroom;

public sealed class AddTeachersToClassroomHandler(IUnitOfWork unitOfWork, ITeacherDirectory teacherDirectory)
    : IHandler<AddTeachersToClassroomCommand, ClassroomDto?>
{
    public async Task<ClassroomDto?> Handle(AddTeachersToClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.ClassroomId, cancellationToken);
        if (classroom is null) return null;

        var requestedTeacherIds = command.TeacherIds ?? [];
        var teacherIds = requestedTeacherIds.Distinct().ToArray();
        if (teacherIds.Length == 0 || teacherIds.Length != requestedTeacherIds.Count)
            throw new InvalidOperationException("Provide one or more unique teacher IDs.");
        if (teacherIds.Any(id => classroom.Teachers.Any(existing => existing.TeacherId == id)))
            throw new InvalidOperationException("One or more teachers are already assigned to this classroom.");

        var unavailable = await teacherDirectory.GetUnavailableTeacherIdsAsync(teacherIds, cancellationToken);
        if (unavailable.Count > 0)
            throw new InvalidOperationException($"These teachers do not exist or are inactive: {string.Join(", ", unavailable)}.");

        foreach (var teacherId in teacherIds)
            classroom.Teachers.Add(new ClassroomTeacher { ClassroomId = classroom.Id, TeacherId = teacherId });

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ToDto(classroom);
    }

    private static ClassroomDto ToDto(Classroom classroom) => new(classroom.Id, classroom.Code,
        classroom.Building, classroom.RoomNumber, classroom.Capacity, classroom.IsActive,
        classroom.CreatedAtUtc, classroom.Students.Select(x => x.StudentId).ToArray(),
        classroom.Teachers.Select(x => x.TeacherId).ToArray());
}
