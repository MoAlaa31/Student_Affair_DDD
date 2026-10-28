namespace Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

public sealed class ClassroomStudentIdsDto
{
    [Required, MinLength(1)]
    public int[] StudentIds { get; set; } = [];
}
