using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Student_Affair_DDD.Modules.Students.Domain.Enums;
using Student_Affair_DDD.Modules.Students.Shared.Abstractions;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;
using Student_Affair_DDD.Web.Client;

namespace BlazorAppServer;

public partial class Students
{
    [Inject]
    public IStudentApi StudentApi { get; set; } = default!;

    public Student? student = new();
    public List<Student>? students = [];
    public Modal? modalComponent;

    private Student? pendingDeleteStudent;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            students = await GetStudentsAsync();
            if (students.Count == 0)
            {
                student = new Student
                {
                    Name = "Wael Shehab Eldin",
                    Mobile = "01207888335",
                    Email = "wael@innotech.com.eg",
                    Age = 44
                };
            }
        }
        catch (Exception exception)
        {
            students = [];
            errorMessage = exception.Message;
        }
    }

    private async Task HandleValidSubmit()
    {
        if (student is null)
            return;

        errorMessage = null;

        try
        {
            if (student.Id == 0)
            {
                await StudentApi.CreateAsync(ToCreateDto(student));
            }
            else if (student.ApiRecord is not null)
            {
                var updated = await StudentApi.UpdateAsync(student.Id, ToUpdateDto(student));
                if (updated is null)
                {
                    errorMessage = "The student no longer exists.";
                    return;
                }
            }

            students = await GetStudentsAsync();
            student = new Student();
        }
        catch (Exception exception)
        {
            errorMessage = exception.Message;
        }
    }

    private async Task<List<Student>> GetStudentsAsync()
    {
        var records = await StudentApi.GetAllAsync();
        return records.Select(record => new Student
        {
            Id = record.Id,
            Name = record.FullName,
            Mobile = record.PhoneNumber ?? string.Empty,
            Email = record.Email,
            Age = GetAge(record.DateOfBirth),
            ApiRecord = record
        }).ToList();
    }

    private void EditStudent(Student item)
    {
        student = new Student
        {
            Id = item.Id,
            Name = item.Name,
            Mobile = item.Mobile,
            Email = item.Email,
            Age = item.Age,
            ApiRecord = item.ApiRecord
        };
    }

    private async Task ShowModal(Student item)
    {
        pendingDeleteStudent = item;
        if (modalComponent is not null)
            await modalComponent.ShowModal();
    }

    private async Task DeleteStudent(Student item)
    {
        var target = pendingDeleteStudent ?? item;
        pendingDeleteStudent = null;
        errorMessage = null;

        try
        {
            var deleted = await StudentApi.DeleteAsync(target.Id);
            if (!deleted)
                errorMessage = "The student no longer exists.";

            students = await GetStudentsAsync();
        }
        catch (Exception exception)
        {
            errorMessage = exception.Message;
        }
    }

    private void Clear()
    {
        student = new Student();
        errorMessage = null;
    }

    private static StudentCreateDto ToCreateDto(Student item)
    {
        var (firstName, lastName) = SplitName(item.Name);
        var token = Guid.NewGuid().ToString("N")[..12].ToUpperInvariant();

        return new StudentCreateDto
        {
            FirstName = firstName,
            LastName = lastName,
            StudentNumber = $"S-{token}",
            NationalId = $"N{token}",
            Email = item.Email,
            PhoneNumber = string.IsNullOrWhiteSpace(item.Mobile) ? null : item.Mobile,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-item.Age)),
            Gender = Gender.Unspecified,
            Level = AcademicLevel.Freshman,
            Department = "General",
            Gpa = 0,
            EnrollmentDate = DateOnly.FromDateTime(DateTime.Today),
            IsActive = true
        };
    }

    private static StudentUpdateDto ToUpdateDto(Student item)
    {
        var source = item.ApiRecord ?? throw new InvalidOperationException("The student record is unavailable.");
        var (firstName, lastName) = SplitName(item.Name);

        return new StudentUpdateDto
        {
            Id = source.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = item.Email,
            PhoneNumber = string.IsNullOrWhiteSpace(item.Mobile) ? null : item.Mobile,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-item.Age)),
            Gender = source.Gender,
            Level = source.Level,
            Department = source.Department,
            Gpa = source.Gpa,
            EnrollmentDate = source.EnrollmentDate,
            IsActive = source.IsActive
        };
    }

    private static (string FirstName, string LastName) SplitName(string name)
    {
        var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            throw new ValidationException("Name is required.");

        return (parts[0], parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : parts[0]);
    }

    private static int GetAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age))
            age--;

        return age;
    }

    public sealed class Student
    {
        public int Id { get; set; }

        [Required, StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Phone, StringLength(20)]
        public string Mobile { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Range(1, 120)]
        public int Age { get; set; }

        public StudentDto? ApiRecord { get; set; }
    }
}