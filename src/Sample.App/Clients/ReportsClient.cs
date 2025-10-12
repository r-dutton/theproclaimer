namespace Sample.App.Clients;

public class ReportsClient
{
    private readonly HttpClient _httpClient;

    public ReportsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> GetSummaryAsync(Guid reportId, CancellationToken cancellationToken)
    {
        var path = $"/api/reports/{reportId}/summary";
        return _httpClient.GetAsync(path, cancellationToken);
    }
}
