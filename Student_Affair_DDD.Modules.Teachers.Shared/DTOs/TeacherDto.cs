namespace Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

public sealed record TeacherDto(
    int Id,
    string EmployeeNumber,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string Department,
    bool IsActive,
    DateTime CreatedAtUtc);
