using System.Net.Http.Json;
using Sample.App.Dtos;

namespace Sample.Web.HttpClients;

public class ReportsClient
{
    private readonly HttpClient _httpClient;

    public ReportsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ReportSummaryDto?> GetReportSummaryAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<ReportSummaryDto>($"/api/reports/{id}", cancellationToken);
    }

    public async Task<IReadOnlyList<ReportSummaryDto>> SearchReportsAsync(string tenantId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/reports/search", new { tenantId }, cancellationToken);
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<ReportSummaryDto>>(cancellationToken: cancellationToken);
        return data ?? new List<ReportSummaryDto>();
    }

    public async Task<ReportDetailsDto?> FetchExternalMetadataAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<ReportDetailsDto>($"/api/v2/reports/{id}/details", cancellationToken);
    }
}
