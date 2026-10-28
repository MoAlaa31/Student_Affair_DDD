namespace Student_Affair_DDD.Modules.Classrooms.Domain;

public sealed class Classroom
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Building { get; set; } = "";
    public string RoomNumber { get; set; } = "";
    public int Capacity { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; }

    public ICollection<ClassroomStudent> Students { get; set; } = new List<ClassroomStudent>();
    public ICollection<ClassroomTeacher> Teachers { get; set; } = new List<ClassroomTeacher>();
}
