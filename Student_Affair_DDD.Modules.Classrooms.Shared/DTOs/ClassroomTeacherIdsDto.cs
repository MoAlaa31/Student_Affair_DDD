namespace Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

public sealed class ClassroomTeacherIdsDto
{
    [Required, MinLength(1)]
    public int[] TeacherIds { get; set; } = [];
}
