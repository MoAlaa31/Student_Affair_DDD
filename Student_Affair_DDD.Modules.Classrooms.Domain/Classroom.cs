namespace Student_Affair_DDD.Modules.Classrooms.Domain;

/// <summary>Classroom aggregate root. Classroom identity and capacity rules belong to this module.</summary>
public sealed class Classroom
{
    private Classroom() { } // EF Core

    public int Id { get; set; }
    public string Code { get; set; } = "";
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; }

    //public static Classroom Create(string code, string building, string roomNumber, int capacity)
    //{
    //    if (capacity is < 1 or > 1000) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be between 1 and 1000.");
    //    return new Classroom
    //    {
    //        Code = Required(code, nameof(code)).ToUpperInvariant(),
    //        Building = Required(building, nameof(building)),
    //        RoomNumber = Required(roomNumber, nameof(roomNumber)),
    //        Capacity = capacity,
    //        IsActive = true,
    //        CreatedAtUtc = DateTime.UtcNow
    //    };
    //}

    //public void Deactivate() => IsActive = false;

    //public void UpdateDetails(string code, string building, string roomNumber, int capacity)
    //{
    //    if (capacity is < 1 or > 1000) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be between 1 and 1000.");
    //    Code = Required(code, nameof(code)).ToUpperInvariant();
    //    Building = Required(building, nameof(building));
    //    RoomNumber = Required(roomNumber, nameof(roomNumber));
    //    Capacity = capacity;
    //}

    //private static string Required(string value, string name)
    //{
    //    var normalized = value?.Trim();
    //    return string.IsNullOrWhiteSpace(normalized) ? throw new ArgumentException("A value is required.", name) : normalized;
    //}
}
