using System.Text.Json.Serialization;

namespace GraphKit.Graph;

public sealed class GraphEdge
{
    [JsonPropertyName("from")]
    public required string From { get; init; }

    [JsonPropertyName("to")]
    public required string To { get; init; }

    [JsonPropertyName("kind")]
    public required string Kind { get; init; }

    [JsonPropertyName("source")]
    public required string Source { get; init; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; } = 1.0;

    [JsonPropertyName("transform")]
    public GraphTransform? Transform { get; init; }

    [JsonPropertyName("props")]
    public IReadOnlyDictionary<string, object>? Props { get; init; }

    [JsonPropertyName("evidence")]
    public GraphEvidence? Evidence { get; init; }
}

public sealed class GraphTransform
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("method")]
    public string? Method { get; init; }

    [JsonPropertyName("location")]
    public GraphLocation? Location { get; init; }

    [JsonPropertyName("method_span")]
    public GraphSpan? MethodSpan { get; init; }

    [JsonPropertyName("il_range")]
    public GraphIlRange? IlRange { get; init; }
}

public sealed class GraphLocation
{
    [JsonPropertyName("file")]
    public required string File { get; init; }

    [JsonPropertyName("line")]
    public required int Line { get; init; }
}

public sealed class GraphIlRange
{
    [JsonPropertyName("start_offset")]
    public int? StartOffset { get; init; }

    [JsonPropertyName("end_offset")]
    public int? EndOffset { get; init; }
}

public sealed class GraphEvidence
{
    [JsonPropertyName("files")]
    public IReadOnlyList<GraphEvidenceFile> Files { get; init; } = Array.Empty<GraphEvidenceFile>();

    [JsonPropertyName("trace_ids")]
    public IReadOnlyList<string> TraceIds { get; init; } = Array.Empty<string>();
}

public sealed class GraphEvidenceFile
{
    [JsonPropertyName("path")]
    public required string Path { get; init; }

    [JsonPropertyName("start_line")]
    public required int StartLine { get; init; }

    [JsonPropertyName("end_line")]
    public required int EndLine { get; init; }
}
