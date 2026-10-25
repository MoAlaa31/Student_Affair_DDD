using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Student_Affair_DDD.Modules.Classrooms.Application.Classrooms;
using Student_Affair_DDD.Modules.Classrooms.Application.Common;
using Student_Affair_DDD.Modules.Classrooms.Application.Services;
using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

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

    [HttpGet]
    [ProducesResponseType(typeof(PagedClassrooms), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedClassrooms>> Get([FromQuery] ClassroomPaginationSpecParams spec, CancellationToken ct)
        => Ok(await service.GetPagedAsync(spec, ct));
}
