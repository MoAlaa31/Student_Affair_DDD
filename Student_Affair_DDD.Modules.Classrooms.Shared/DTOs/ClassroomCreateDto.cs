namespace Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

public sealed class ClassroomCreateDto
{
    [Required, StringLength(20, MinimumLength = 1)]
    public string Code { get; set; } = "";

    [Required, StringLength(100, MinimumLength = 1)]
    public string Building { get; set; } = "";

    [Required, StringLength(20, MinimumLength = 1)]
    public string RoomNumber { get; set; } = "";

    [Range(1, 1000)]
    public int Capacity { get; set; }

    [Required, MinLength(1, ErrorMessage = "Select at least one student.")]
    public int[] StudentIds { get; set; } = [];

    [Required, MinLength(1, ErrorMessage = "Select at least one teacher.")]
    public int[] TeacherIds { get; set; } = [];
}
