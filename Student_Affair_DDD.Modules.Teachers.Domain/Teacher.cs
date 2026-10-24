namespace Student_Affair_DDD.Modules.Teachers.Domain;

/// <summary>Teacher aggregate root. Staff identity and employment details belong to this module.</summary>
public sealed class Teacher
{
    private Teacher() { } // EF Core

    public int Id { get; private set; }
    public string EmployeeNumber { get; private set; } = "";
    public string FirstName { get; private set; } = "";
    public string LastName { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string Department { get; private set; } = "";
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAtUtc { get; private set; }
    public string FullName => $"{FirstName} {LastName}".Trim();

    public static Teacher Hire(string employeeNumber, string firstName, string lastName, string email, string department)
    {
        var normalizedEmail = Required(email, nameof(email)).ToLowerInvariant();
        if (!normalizedEmail.Contains('@')) throw new ArgumentException("A valid e-mail address is required.", nameof(email));
        return new Teacher
        {
            EmployeeNumber = Required(employeeNumber, nameof(employeeNumber)).ToUpperInvariant(),
            FirstName = Required(firstName, nameof(firstName)),
            LastName = Required(lastName, nameof(lastName)),
            Email = normalizedEmail,
            Department = Required(department, nameof(department)),
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public void Deactivate() => IsActive = false;

    private static string Required(string value, string name)
    {
        var normalized = value?.Trim();
        return string.IsNullOrWhiteSpace(normalized) ? throw new ArgumentException("A value is required.", name) : normalized;
    }
}
