using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Student_Affair_DDD.Modules.Teachers.Application.Teachers;
using Student_Affair_DDD.Modules.Teachers.Application.Common;
using Student_Affair_DDD.Modules.Teachers.Application.Services;
using Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Teachers.Web;

[ApiController]
[Route("api/teachers")]
[Produces("application/json")]
public sealed class TeachersController(ITeacherService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(TeacherDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TeacherDto>> Create([FromBody] TeacherCreateDto request, CancellationToken ct)
    {
        try
        {
            var created = await service.CreateAsync(request, ct);
            return StatusCode(StatusCodes.Status201Created, created);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Teacher conflict", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedTeachers), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedTeachers>> Get([FromQuery] TeacherPaginationSpecParams spec, CancellationToken ct)
        => Ok(await service.GetPagedAsync(spec, ct));
}
