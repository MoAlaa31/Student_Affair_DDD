namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.CheckTeacherEmployeeNumberAvailability;

public sealed record CheckTeacherEmployeeNumberAvailabilityQuery(string EmployeeNumber, int? ExcludeId = null) : IRequest<bool>;
