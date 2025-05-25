using APBD_Tutorial11.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Tutorial11.Presentation;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetPatientQuery {Id = id});
        return result != null ? Ok(result) : NotFound(new { message = "Patient not found." });
    }
}