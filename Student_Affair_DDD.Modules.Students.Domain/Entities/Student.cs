using Student_Affair_DDD.Modules.Students.Domain.Enums;

namespace Student_Affair_DDD.Modules.Students.Domain.Entities;
/// <summary>Student aggregate root. State changes go through domain methods.</summary>
public sealed class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string StudentNumber { get; set; } = "";
    public string NationalId { get; set; } = "";
    public string Email { get; set; } = "";
    public string? PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public AcademicLevel Level { get; set; }
    public string Department { get; set; } = "";
    public decimal Gpa { get; set; }
    public DateOnly EnrollmentDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public string FullName => $"{FirstName} {LastName}".Trim();

    public static Student Register(string firstName, string lastName, string studentNumber,
        string nationalId, string email, string? phoneNumber, DateOnly dateOfBirth,
        Gender gender, AcademicLevel level, string department, decimal gpa, DateOnly enrollmentDate)
    {
        firstName = Required(firstName, nameof(firstName));
        lastName = Required(lastName, nameof(lastName));
        studentNumber = Required(studentNumber, nameof(studentNumber));
        nationalId = Required(nationalId, nameof(nationalId));
        email = Required(email, nameof(email)).ToLowerInvariant();
        department = Required(department, nameof(department));
        if (gpa is < 0 or > 4) throw new ArgumentOutOfRangeException(nameof(gpa), "GPA must be between 0 and 4.");
        if (dateOfBirth >= DateOnly.FromDateTime(DateTime.UtcNow)) throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));
        if (!Enum.IsDefined(gender) || !Enum.IsDefined(level)) throw new ArgumentException("Gender or academic level is invalid.");

        return new Student
        {
            FirstName = firstName, LastName = lastName, StudentNumber = studentNumber,
            NationalId = nationalId, Email = email, PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth, Gender = gender, Level = level,
            Department = department, Gpa = gpa, EnrollmentDate = enrollmentDate,
            IsActive = true, CreatedAtUtc = DateTime.UtcNow
        };
    }

    public void ChangeAcademicRecord(AcademicLevel level, string department, decimal gpa)
    {
        if (!Enum.IsDefined(level)) throw new ArgumentOutOfRangeException(nameof(level));
        if (gpa is < 0 or > 4) throw new ArgumentOutOfRangeException(nameof(gpa), "GPA must be between 0 and 4.");
        Department = Required(department, nameof(department));
        Level = level;
        Gpa = gpa;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    private static string Required(string value, string name)
    {
        var normalized = value?.Trim();
        return string.IsNullOrWhiteSpace(normalized) ? throw new ArgumentException("A value is required.", name) : normalized;
    }
}
