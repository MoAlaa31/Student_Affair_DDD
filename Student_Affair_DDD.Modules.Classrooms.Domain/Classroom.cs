namespace Student_Affair_DDD.Modules.Classrooms.Domain;

/// <summary>Classroom aggregate root. Classroom identity and capacity rules belong to this module.</summary>
public sealed class Classroom
{
    private Classroom() { } // EF Core

    public int Id { get; private set; }
    public string Code { get; private set; } = "";
    public string Building { get; private set; } = "";
    public string RoomNumber { get; private set; } = "";
    public int Capacity { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAtUtc { get; private set; }

    public static Classroom Create(string code, string building, string roomNumber, int capacity)
    {
        if (capacity is < 1 or > 1000) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be between 1 and 1000.");
        return new Classroom
        {
            Code = Required(code, nameof(code)).ToUpperInvariant(),
            Building = Required(building, nameof(building)),
            RoomNumber = Required(roomNumber, nameof(roomNumber)),
            Capacity = capacity,
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
