using System.ComponentModel.DataAnnotations;

namespace Student_Affair_DDD.Contracts;

public enum Gender { Unspecified = 0, Male = 1, Female = 2 }
public enum AcademicLevel { Freshman = 1, Sophomore = 2, Junior = 3, Senior = 4, Graduate = 5 }

public sealed class CreateStudentRequest
{
    [Required, StringLength(100, MinimumLength = 2)] public string FirstName { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 2)] public string LastName { get; set; } = "";
    [Required, StringLength(20, MinimumLength = 3)] public string StudentNumber { get; set; } = "";
    [Required, StringLength(20, MinimumLength = 5)] public string NationalId { get; set; } = "";
    [Required, EmailAddress, StringLength(200)] public string Email { get; set; } = "";
    [Phone, StringLength(20)] public string? PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public AcademicLevel Level { get; set; } = AcademicLevel.Freshman;
    [Required, StringLength(100, MinimumLength = 2)] public string Department { get; set; } = "";
    [Range(0, 4)] public decimal Gpa { get; set; }
    public DateOnly EnrollmentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
}

public sealed record StudentResponse(
    int Id, string FirstName, string LastName, string FullName, string StudentNumber,
    string NationalId, string Email, string? PhoneNumber, DateOnly DateOfBirth,
    Gender Gender, AcademicLevel Level, string Department, decimal Gpa,
    DateOnly EnrollmentDate, bool IsActive, DateTime CreatedAtUtc);

