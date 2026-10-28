namespace Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

public sealed class TeacherCreateDto
{
    [Required, StringLength(20, MinimumLength = 2)]
    public string EmployeeNumber { get; set; } = "";

    [Required, StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; } = "";

    [Required, StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; } = "";

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = "";

    [Required, StringLength(100, MinimumLength = 2)]
    public string Department { get; set; } = "";
}
