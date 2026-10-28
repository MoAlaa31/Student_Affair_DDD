namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.UpdateClassroom;

public sealed class UpdateClassroomHandler(
    IUnitOfWork unitOfWork,
    IStudentDirectory studentDirectory,
    ITeacherDirectory teacherDirectory) : IHandler<UpdateClassroomCommand, ClassroomDto?>
{
    public async Task<ClassroomDto?> Handle(UpdateClassroomCommand command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.Id, cancellationToken);
        if (classroom is null) return null;

        var request = command.Request;
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
        if (await unitOfWork.Classrooms.CodeExistsAsync(request.Code, command.Id, cancellationToken))
            throw new InvalidOperationException("A classroom with that code already exists.");

        var existingStudentIds = classroom.Students.Select(x => x.StudentId).ToHashSet();
        var existingTeacherIds = classroom.Teachers.Select(x => x.TeacherId).ToHashSet();
        var addedStudentIds = studentIds.Where(id => !existingStudentIds.Contains(id)).ToArray();
        var addedTeacherIds = teacherIds.Where(id => !existingTeacherIds.Contains(id)).ToArray();

        var unavailableStudents = await studentDirectory.GetUnavailableStudentIdsAsync(addedStudentIds, cancellationToken);
        if (unavailableStudents.Count > 0)
            throw new InvalidOperationException($"These students do not exist or are inactive: {string.Join(", ", unavailableStudents)}.");

        var unavailableTeachers = await teacherDirectory.GetUnavailableTeacherIdsAsync(addedTeacherIds, cancellationToken);
        if (unavailableTeachers.Count > 0)
            throw new InvalidOperationException($"These teachers do not exist or are inactive: {string.Join(", ", unavailableTeachers)}.");

        classroom.Code = request.Code.Trim().ToUpperInvariant();
        classroom.Building = request.Building.Trim();
        classroom.RoomNumber = request.RoomNumber.Trim();
        classroom.Capacity = request.Capacity;
        var requestedStudentSet = studentIds.ToHashSet();
        foreach (var assignment in classroom.Students.Where(x => !requestedStudentSet.Contains(x.StudentId)).ToArray())
            classroom.Students.Remove(assignment);
        foreach (var studentId in addedStudentIds)
            classroom.Students.Add(new ClassroomStudent { ClassroomId = classroom.Id, StudentId = studentId });
        var requestedTeacherSet = teacherIds.ToHashSet();
        foreach (var assignment in classroom.Teachers.Where(x => !requestedTeacherSet.Contains(x.TeacherId)).ToArray())
            classroom.Teachers.Remove(assignment);
        foreach (var teacherId in addedTeacherIds)
            classroom.Teachers.Add(new ClassroomTeacher { ClassroomId = classroom.Id, TeacherId = teacherId });
        unitOfWork.Classrooms.Update(classroom);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new ClassroomDto(classroom.Id, classroom.Code, classroom.Building, classroom.RoomNumber,
            classroom.Capacity, classroom.IsActive, classroom.CreatedAtUtc,
            classroom.Students.Select(x => x.StudentId).ToArray(),
            classroom.Teachers.Select(x => x.TeacherId).ToArray());
    }
}
