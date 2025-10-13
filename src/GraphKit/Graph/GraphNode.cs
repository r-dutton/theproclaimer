using System.Text.Json.Serialization;

namespace GraphKit.Graph;

public sealed class GraphNode
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("fqdn")]
    public required string Fqdn { get; init; }

    [JsonPropertyName("assembly")]
    public required string Assembly { get; init; }

    [JsonPropertyName("project")]
    public required string Project { get; init; }

    [JsonPropertyName("file_path")]
    public required string FilePath { get; init; }

    [JsonPropertyName("span")]
    public GraphSpan? Span { get; init; }

    [JsonPropertyName("symbol_id")]
    public required string SymbolId { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<string> Tags { get; init; } = Array.Empty<string>();

    [JsonPropertyName("props")]
    public IReadOnlyDictionary<string, object>? Props { get; init; }
}

public sealed class GraphSpan
{
    [JsonPropertyName("start_line")]
    public required int StartLine { get; init; }

    [JsonPropertyName("end_line")]
    public required int EndLine { get; init; }
}
