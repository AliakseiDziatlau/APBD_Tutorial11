using APBD_Tutorial11.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Tutorial11.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrescriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(CreatePrescriptionCommand command)
    {
        var result = await _mediator.Send(command);
        
        return result.Match<IActionResult>(
            success => Ok(new { success.Message }),
            notFound => NotFound(new { notFound.Message }),
            badRequest => BadRequest(new { badRequest.Message })
        );
    }
}