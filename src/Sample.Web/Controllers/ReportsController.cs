using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.App.Dtos;
using Sample.App.Requests;
using Sample.App.Validation;

namespace Sample.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly GetReportQueryValidator _validator;

    public ReportsController(IMediator mediator, GetReportQueryValidator validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReportDto>> Get(Guid id, [FromHeader(Name = "X-Tenant-Id")] Guid tenantId, CancellationToken cancellationToken)
    {
        var query = new GetReportQuery(id, tenantId);
        var validation = await _validator.ValidateAsync(query, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation.ToDictionary());
        }

        var dto = await _mediator.Send(query, cancellationToken);
        return Ok(dto);
    }
}
