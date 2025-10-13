using System.Text.Json.Serialization;

namespace GraphKit.Graph;

public sealed class GraphDocument
{
    [JsonPropertyName("version")]
    public required string Version { get; init; }

    [JsonPropertyName("nodes")]
    public IReadOnlyList<GraphNode> Nodes { get; init; } = Array.Empty<GraphNode>();

    [JsonPropertyName("edges")]
    public IReadOnlyList<GraphEdge> Edges { get; init; } = Array.Empty<GraphEdge>();
}
