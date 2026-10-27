namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.CheckTeacherEmailAvailability;

public sealed record CheckTeacherEmailAvailabilityQuery(string Email, int? ExcludeId = null) : IRequest<bool>;
