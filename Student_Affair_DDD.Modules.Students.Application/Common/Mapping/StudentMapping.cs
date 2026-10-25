

namespace Student_Affair_DDD.Modules.Students.Application.Common.Mapping;

internal static class StudentMappings
{
    public static StudentDto ToDto(this Student e) => new()
    {
        Id = e.Id,
        FirstName = e.FirstName,
        LastName = e.LastName,
        FullName = e.FullName,
        StudentNumber = e.StudentNumber,
        NationalId = e.NationalId,
        Email = e.Email,
        PhoneNumber = e.PhoneNumber,
        DateOfBirth = e.DateOfBirth,
        Gender = e.Gender,
        Level = e.Level,
        Department = e.Department,
        Gpa = e.Gpa,
        EnrollmentDate = e.EnrollmentDate,
        IsActive = e.IsActive,
        CreatedAtUtc = e.CreatedAtUtc,
        UpdatedAtUtc = e.UpdatedAtUtc
    };

    public static Student ToEntity(this StudentCreateDto d) => new()
    {
        FirstName = d.FirstName.Trim(),
        LastName = d.LastName.Trim(),
        StudentNumber = d.StudentNumber.Trim(),
        NationalId = d.NationalId.Trim(),
        Email = d.Email.Trim(),
        PhoneNumber = string.IsNullOrWhiteSpace(d.PhoneNumber) ? null : d.PhoneNumber.Trim(),
        DateOfBirth = d.DateOfBirth,
        Gender = d.Gender,
        Level = d.Level,
        Department = d.Department.Trim(),
        Gpa = d.Gpa,
        EnrollmentDate = d.EnrollmentDate,
        IsActive = d.IsActive
    };

    public static void Apply(this StudentUpdateDto d, Student e)
    {
        e.FirstName = d.FirstName.Trim();
        e.LastName = d.LastName.Trim();
        e.Email = d.Email.Trim();
        e.PhoneNumber = string.IsNullOrWhiteSpace(d.PhoneNumber) ? null : d.PhoneNumber.Trim();
        e.DateOfBirth = d.DateOfBirth;
        e.Gender = d.Gender;
        e.Level = d.Level;
        e.Department = d.Department.Trim();
        e.Gpa = d.Gpa;
        e.EnrollmentDate = d.EnrollmentDate;
        e.IsActive = d.IsActive;
    }
}
