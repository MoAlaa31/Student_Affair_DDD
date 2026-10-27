namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.CheckClassroomCodeAvailability;

public sealed record CheckClassroomCodeAvailabilityQuery(string Code, int? ExcludeId = null) : IRequest<bool>;
