using System.ComponentModel.DataAnnotations;

namespace Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

public sealed record TeacherDto(int Id, string EmployeeNumber, string FirstName, string LastName, string FullName, string Email, string Department, bool IsActive, DateTime CreatedAtUtc);

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

public sealed class TeacherUpdateDto
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

public sealed class TeacherQuery
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
    public string? Department { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}

public sealed record PagedTeachers(IReadOnlyList<TeacherDto> Items, int TotalCount, int PageIndex, int PageSize);
