using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Student_Affair_DDD.Modules.Classrooms.Application.Services;
using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;
using Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;

namespace Student_Affair_DDD.Modules.Classrooms.Web;

[ApiController]
[Route("api/classrooms")]
[Produces("application/json")]
public sealed class ClassroomsController(IClassroomService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ClassroomDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ClassroomDto>> Create([FromBody] ClassroomCreateDto request, CancellationToken ct)
    {
        try
        {
            var created = await service.CreateAsync(request, ct);
            return StatusCode(StatusCodes.Status201Created, created);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Classroom conflict", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClassroomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassroomDto>> GetById(int id, CancellationToken ct)
    {
        var classroom = await service.GetByIdAsync(id, ct);
        return classroom is null ? NotFound() : Ok(classroom);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ClassroomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ClassroomDto>> Update(int id, [FromBody] ClassroomUpdateDto request, CancellationToken ct)
    {
        try
        {
            var updated = await service.UpdateAsync(id, request, ct);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Classroom conflict", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpPost("{id:int}/students")]
    [ProducesResponseType(typeof(ClassroomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ClassroomDto>> AddStudents(int id, [FromBody] ClassroomStudentIdsDto request, CancellationToken ct)
    {
        try
        {
            var classroom = await service.AddStudentsAsync(id, request, ct);
            return classroom is null ? NotFound() : Ok(classroom);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Student assignment conflict", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpPost("{id:int}/teachers")]
    [ProducesResponseType(typeof(ClassroomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ClassroomDto>> AddTeachers(int id, [FromBody] ClassroomTeacherIdsDto request, CancellationToken ct)
    {
        try
        {
            var classroom = await service.AddTeachersAsync(id, request, ct);
            return classroom is null ? NotFound() : Ok(classroom);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Teacher assignment conflict", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpDelete("{id:int}/students/{studentId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RemoveStudent(int id, int studentId, CancellationToken ct)
    {
        try
        {
            return await service.RemoveStudentAsync(id, studentId, ct) ? NoContent() : NotFound();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Student assignment conflict", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpDelete("{id:int}/teachers/{teacherId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RemoveTeacher(int id, int teacherId, CancellationToken ct)
    {
        try
        {
            return await service.RemoveTeacherAsync(id, teacherId, ct) ? NoContent() : NotFound();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Teacher assignment conflict", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedClassrooms), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedClassrooms>> Get([FromQuery] ClassroomPaginationSpecParams spec, CancellationToken ct)
        => Ok(await service.GetPagedAsync(spec, ct));
}
