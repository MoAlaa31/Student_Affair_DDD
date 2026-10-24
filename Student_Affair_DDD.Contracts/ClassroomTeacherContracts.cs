using System.ComponentModel.DataAnnotations;

namespace Student_Affair_DDD.Contracts;

public sealed class CreateClassroomRequest
{
    [Required, StringLength(20, MinimumLength = 1)] public string Code { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 1)] public string Building { get; set; } = "";
    [Required, StringLength(20, MinimumLength = 1)] public string RoomNumber { get; set; } = "";
    [Range(1, 1000)] public int Capacity { get; set; }
}

public sealed record ClassroomResponse(int Id, string Code, string Building, string RoomNumber, int Capacity, bool IsActive, DateTime CreatedAtUtc);

public sealed class CreateTeacherRequest
{
    [Required, StringLength(20, MinimumLength = 2)] public string EmployeeNumber { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 2)] public string FirstName { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 2)] public string LastName { get; set; } = "";
    [Required, EmailAddress, StringLength(200)] public string Email { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 2)] public string Department { get; set; } = "";
}

public sealed record TeacherResponse(int Id, string EmployeeNumber, string FirstName, string LastName, string FullName, string Email, string Department, bool IsActive, DateTime CreatedAtUtc);
