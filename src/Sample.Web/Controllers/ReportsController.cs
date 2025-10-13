using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.App.Commands;
using Sample.App.Dtos;
using Sample.App.Queries;
using Sample.Web.HttpClients;

namespace Sample.Web.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<ReportDto> _validator;
    private readonly ReportsClient _reportsClient;
    private readonly IMapper _mapper;

    public ReportsController(IMediator mediator, IValidator<ReportDto> validator, ReportsClient reportsClient, IMapper mapper)
    {
        _mediator = mediator;
        _validator = validator;
        _reportsClient = reportsClient;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReportDto>> GetReport(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetReportQuery(id), cancellationToken);
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);
        return Ok(dto);
    }

    [HttpGet("{id:guid}/summary")]
    public async Task<ActionResult<ReportSummaryDto>> GetReportSummary(Guid id, CancellationToken cancellationToken)
    {
        var summary = await _reportsClient.GetReportSummaryAsync(id, cancellationToken);
        if (summary is null)
        {
            return NotFound();
        }

        return Ok(summary);
    }

    [HttpGet("tenant/{tenantId}")]
    public async Task<ActionResult<IReadOnlyList<ReportSummaryDto>>> SearchTenantReports(string tenantId, CancellationToken cancellationToken)
    {
        var summaries = await _reportsClient.SearchReportsAsync(tenantId, cancellationToken);
        return Ok(summaries);
    }

    [HttpPost]
    public async Task<ActionResult<ReportDto>> CreateDetailedReport([FromBody] CreateReportDto dto, CancellationToken cancellationToken)
    {
        var specialized = dto as CreateDetailedReportDto;
        if (specialized is null)
        {
            return BadRequest("Detailed report payload is required.");
        }

        if (specialized is not { Summary: { Length: > 0 } } detailedDto)
        {
            return BadRequest("Detailed report summary is required.");
        }

        var command = _mapper.Map<CreateDetailedReportCommand>(detailedDto);
        var created = await _mediator.Send(command, cancellationToken);
        var external = await _reportsClient.FetchExternalMetadataAsync(created.Id, cancellationToken);
        var enriched = external is null ? created : created with { Summary = external.Summary };
        await _validator.ValidateAndThrowAsync(enriched, cancellationToken);
        return CreatedAtAction(nameof(GetReport), new { id = enriched.Id }, enriched);
    }
}
