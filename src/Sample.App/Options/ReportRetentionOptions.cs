namespace Sample.App.Options;

public sealed class ReportRetentionOptions
{
    public const string SectionName = "Retention";

    public int RetentionDays { get; set; } = 30;

    public int PollingIntervalMinutes { get; set; } = 30;

    public string CachePrefix { get; set; } = "report";

    public int CacheExpirationMinutes { get; set; } = 5;
}
