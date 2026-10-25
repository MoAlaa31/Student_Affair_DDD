using System.ComponentModel.DataAnnotations;
using Student_Affair_DDD.Modules.Students.Domain.Enums;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;

namespace Student_Affair_DDD.Web.Client.Models;

/// <summary>Editable fields required by the Students API for creating or updating a student.</summary>
public sealed class StudentFormModel : IValidatableObject
{
    public int Id { get; set; }
    public string FullName => $"{FirstName} {LastName}".Trim();

    [Required, StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(20, MinimumLength = 3)]
    [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Only letters, digits and dashes are allowed.")]
    public string StudentNumber { get; set; } = string.Empty;

    [Required, StringLength(20, MinimumLength = 5)]
    public string NationalId { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; } = new(2005, 1, 1);

    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; } = Gender.Unspecified;

    [EnumDataType(typeof(AcademicLevel))]
    public AcademicLevel Level { get; set; } = AcademicLevel.Freshman;

    [Required, StringLength(100, MinimumLength = 2)]
    public string Department { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "4", ErrorMessage = "GPA must be between 0.00 and 4.00.")]
    public decimal Gpa { get; set; }

    [Required]
    public DateOnly EnrollmentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public bool IsActive { get; set; } = true;

    public static StudentFormModel FromDto(StudentDto dto) => new()
    {
        Id = dto.Id,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        StudentNumber = dto.StudentNumber,
        NationalId = dto.NationalId,
        Email = dto.Email,
        PhoneNumber = dto.PhoneNumber,
        DateOfBirth = dto.DateOfBirth,
        Gender = dto.Gender,
        Level = dto.Level,
        Department = dto.Department,
        Gpa = dto.Gpa,
        EnrollmentDate = dto.EnrollmentDate,
        IsActive = dto.IsActive
    };

    public StudentCreateDto ToCreateDto() => new()
    {
        FirstName = FirstName.Trim(),
        LastName = LastName.Trim(),
        StudentNumber = StudentNumber.Trim(),
        NationalId = NationalId.Trim(),
        Email = Email.Trim(),
        PhoneNumber = string.IsNullOrWhiteSpace(PhoneNumber) ? null : PhoneNumber.Trim(),
        DateOfBirth = DateOfBirth,
        Gender = Gender,
        Level = Level,
        Department = Department.Trim(),
        Gpa = Gpa,
        EnrollmentDate = EnrollmentDate,
        IsActive = IsActive
    };

    public StudentUpdateDto ToUpdateDto() => new()
    {
        Id = Id,
        FirstName = FirstName.Trim(),
        LastName = LastName.Trim(),
        Email = Email.Trim(),
        PhoneNumber = string.IsNullOrWhiteSpace(PhoneNumber) ? null : PhoneNumber.Trim(),
        DateOfBirth = DateOfBirth,
        Gender = Gender,
        Level = Level,
        Department = Department.Trim(),
        Gpa = Gpa,
        EnrollmentDate = EnrollmentDate,
        IsActive = IsActive
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateOfBirth >= DateOnly.FromDateTime(DateTime.Today))
            yield return new ValidationResult("Date of birth must be in the past.", [nameof(DateOfBirth)]);

        if (EnrollmentDate == default)
            yield return new ValidationResult("Enrollment date is required.", [nameof(EnrollmentDate)]);
        else if (EnrollmentDate < DateOfBirth)
            yield return new ValidationResult("Enrollment date cannot be before the date of birth.", [nameof(EnrollmentDate)]);

        if (!Enum.IsDefined(Gender))
            yield return new ValidationResult("Select a valid gender.", [nameof(Gender)]);

        if (!Enum.IsDefined(Level))
            yield return new ValidationResult("Select a valid academic level.", [nameof(Level)]);
    }
}
