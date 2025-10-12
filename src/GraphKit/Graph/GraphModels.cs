using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace GraphKit.Graph;

public sealed class GraphDocument
{
    [JsonPropertyName("nodes")]
    public List<GraphNode> Nodes { get; } = new();

    [JsonPropertyName("edges")]
    public List<GraphEdge> Edges { get; } = new();

    [JsonPropertyName("schema_version")]
    public string SchemaVersion { get; init; } = "1.0";

    [JsonPropertyName("analyzer_version")]
    public string AnalyzerVersion { get; init; } = "graphkit-1.0.0";

    public void SortDeterministically()
    {
        Nodes.Sort((a, b) => string.CompareOrdinal(a.Id, b.Id));
        Edges.Sort((a, b) =>
        {
            var cmp = string.CompareOrdinal(a.From, b.From);
            if (cmp != 0)
            {
                return cmp;
            }

            cmp = string.CompareOrdinal(a.To, b.To);
            if (cmp != 0)
            {
                return cmp;
            }

            cmp = string.CompareOrdinal(a.Kind, b.Kind);
            if (cmp != 0)
            {
                return cmp;
            }

            cmp = string.CompareOrdinal(a.Transform.Type, b.Transform.Type);
            if (cmp != 0)
            {
                return cmp;
            }

            return string.CompareOrdinal(a.Transform.Method ?? string.Empty, b.Transform.Method ?? string.Empty);
        });
    }
}

public sealed class GraphNode
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("fqdn")]
    public string Fqdn { get; init; } = string.Empty;

    [JsonPropertyName("assembly")]
    public string Assembly { get; init; } = string.Empty;

    [JsonPropertyName("project")]
    public string Project { get; init; } = string.Empty;

    [JsonPropertyName("file_path")]
    public string FilePath { get; init; } = string.Empty;

    [JsonPropertyName("span")]
    public GraphSpan? Span { get; init; }

    [JsonPropertyName("symbol_id")]
    public string SymbolId { get; init; } = string.Empty;

    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; } = new();

    [JsonPropertyName("props")]
    public Dictionary<string, object?> Props { get; init; } = new();
}

public sealed class GraphSpan
{
    [JsonPropertyName("start_line")]
    public int StartLine { get; init; }

    [JsonPropertyName("end_line")]
    public int EndLine { get; init; }
}

public sealed class GraphEdge
{
    [JsonPropertyName("from")]
    public string From { get; init; } = string.Empty;

    [JsonPropertyName("to")]
    public string To { get; init; } = string.Empty;

    [JsonPropertyName("kind")]
    public string Kind { get; init; } = string.Empty;

    [JsonPropertyName("source")]
    public string Source { get; init; } = "static";

    [JsonPropertyName("confidence")]
    public double Confidence { get; init; } = 1.0;

    [JsonPropertyName("transform")]
    public GraphTransform Transform { get; init; } = new();

    [JsonPropertyName("props")]
    public Dictionary<string, object?> Props { get; init; } = new();

    [JsonPropertyName("evidence")]
    public GraphEvidence Evidence { get; init; } = new();
}

public sealed class GraphTransform
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

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
    public string File { get; init; } = string.Empty;

    [JsonPropertyName("line")]
    public int Line { get; init; }
}

public sealed class GraphIlRange
{
    [JsonPropertyName("start_offset")]
    public int StartOffset { get; init; }

    [JsonPropertyName("end_offset")]
    public int EndOffset { get; init; }
}

public sealed class GraphEvidence
{
    [JsonPropertyName("files")]
    public List<GraphEvidenceFile> Files { get; init; } = new();

    [JsonPropertyName("trace_ids")]
    public List<string> TraceIds { get; init; } = new();
}

public sealed class GraphEvidenceFile
{
    [JsonPropertyName("path")]
    public string Path { get; init; } = string.Empty;

    [JsonPropertyName("start_line")]
    public int StartLine { get; init; }

    [JsonPropertyName("end_line")]
    public int EndLine { get; init; }
}

public static class HashUtilities
{
    public static string CreateNodeId(string type, string fqdn, string assembly, string symbolId)
    {
        var payload = $"{type}|{fqdn}|{assembly}|{symbolId}";
        return "node::" + ComputeStableHash(payload);
    }

    public static string ComputeStableHash(string value)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(value);
        var hash = sha.ComputeHash(bytes);
        var builder = new StringBuilder(hash.Length * 2);
        foreach (var b in hash)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}
