using GraphKit.Analyzers;
using Xunit;

namespace GraphKit.Tests.Analyzers;

public sealed class ProjectAnalyzerPruningTests
{
    [Theory]
    [InlineData("Microsoft.Extensions.Logging.ILogger", true)]
    [InlineData("SampleProject.Infrastructure.Logger", true)]
    [InlineData("Serilog.Core.Logger", true)]
    [InlineData("SampleProject.Services.TelemetryService", false)]
    [InlineData(null, false)]
    public void IsLoggerType_RecognizesLoggerPatterns(string? typeName, bool expected)
    {
        Assert.Equal(expected, ProjectAnalyzer.IsLoggerType(typeName));
    }

    [Theory]
    [InlineData("Sample.TelemetryClient", true)]
    [InlineData("System.Diagnostics.ActivitySource", true)]
    [InlineData("OpenTelemetry.Trace.Tracer", true)]
    [InlineData("Sample.Services.LoggingService", false)]
    [InlineData(null, false)]
    public void IsTelemetryType_RecognizesTelemetryPatterns(string? typeName, bool expected)
    {
        Assert.Equal(expected, ProjectAnalyzer.IsTelemetryType(typeName));
    }
}
