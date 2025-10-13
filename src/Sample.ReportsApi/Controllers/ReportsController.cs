using System;
using Microsoft.AspNetCore.Mvc;

namespace Sample.ReportsApi.Controllers;

[ApiController]
[Route("api/v2/reports")]
public class ReportsController : ControllerBase
{
    [HttpGet("{id:guid}/details")]
    public ActionResult<ExternalReportDetails> GetDetails(Guid id)
    {
        var details = new ExternalReportDetails(id, $"Detailed insights for {id}", DateTimeOffset.UtcNow);
        return Ok(details);
    }

    public record ExternalReportDetails(Guid Id, string Summary, DateTimeOffset LastSynced);
}
