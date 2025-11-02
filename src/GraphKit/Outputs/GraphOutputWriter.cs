
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Outputs.Abstractions;
using GraphKit.Outputs.FlowBuilder;
using GraphKit.Outputs.Legacy;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

public sealed class GraphOutputWriter
{
    private readonly string _workspaceRoot;
    private readonly string _outputDirectory;
    private readonly FlowWorkspaceIndex _workspaceIndex;
    private static readonly JsonSerializerOptions CypherSerializerOptions = new()
    {
        WriteIndented = false
    };

    public GraphOutputWriter(string workspaceRoot, string outputDirectory)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        _outputDirectory = Path.GetFullPath(Path.Combine(_workspaceRoot, outputDirectory));
        _workspaceIndex = FlowWorkspaceIndex.Load(_workspaceRoot);
    }

    public async Task WriteAsync(GraphDocument document, string analyzerVersion, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_outputDirectory);
        await WriteGraphJsonAsync(document, cancellationToken);
        await WriteGraphCypherAsync(document, cancellationToken);
        await WriteGraphMarkdownAsync(document, analyzerVersion, cancellationToken);
        await WriteControllerFlowsAsync(document, cancellationToken);
        await WriteVersionAsync(analyzerVersion, cancellationToken);
        await WriteEvalAsync(document, cancellationToken);
    }

    private async Task WriteGraphJsonAsync(GraphDocument document, CancellationToken cancellationToken)
    {
        var jsonPath = Path.Combine(_outputDirectory, "graph.json");
        await using var stream = File.Create(jsonPath);
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
        writer.WriteStartObject();
        writer.WriteString("version", document.Version);
        writer.WritePropertyName("nodes");
        writer.WriteStartArray();
        foreach (var node in document.Nodes)
        {
            WriteNode(writer, node);
        }
        writer.WriteEndArray();
        writer.WritePropertyName("edges");
        writer.WriteStartArray();
        foreach (var edge in document.Edges)
        {
            WriteEdge(writer, edge);
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
        await writer.FlushAsync(cancellationToken);
    }

    private static void WriteNode(Utf8JsonWriter w, GraphNode n)
    {
        w.WriteStartObject();
        w.WriteString("id", n.Id);
        w.WriteString("type", n.Type);
        if (!string.IsNullOrWhiteSpace(n.Name)) w.WriteString("name", n.Name);
        if (!string.IsNullOrWhiteSpace(n.Fqdn)) w.WriteString("fqdn", n.Fqdn);
        if (!string.IsNullOrWhiteSpace(n.Assembly)) w.WriteString("assembly", n.Assembly);
        if (!string.IsNullOrWhiteSpace(n.Project)) w.WriteString("project", n.Project);
        if (!string.IsNullOrWhiteSpace(n.FilePath)) w.WriteString("file_path", n.FilePath);
        if (!string.IsNullOrWhiteSpace(n.SymbolId)) w.WriteString("symbol_id", n.SymbolId);
        if (n.Tags is { Count: > 0 })
        {
            w.WritePropertyName("tags");
            w.WriteStartArray();
            foreach (var t in n.Tags) w.WriteStringValue(t);
            w.WriteEndArray();
        }
        if (n.Span is { } span)
        {
            w.WritePropertyName("span");
            w.WriteStartObject();
            w.WriteNumber("start_line", span.StartLine);
            w.WriteNumber("end_line", span.EndLine);
            w.WriteEndObject();
        }
        if (n.Props is { Count: > 0 })
        {
            w.WritePropertyName("props");
            w.WriteStartObject();
            foreach (var kv in n.Props.OrderBy(k => k.Key, StringComparer.Ordinal))
            {
                WritePropValue(w, kv.Key, kv.Value);
            }
            w.WriteEndObject();
        }
        w.WriteEndObject();
    }

    private static void WriteEdge(Utf8JsonWriter w, GraphEdge e)
    {
        w.WriteStartObject();
        w.WriteString("from", e.From);
        w.WriteString("to", e.To);
        w.WriteString("kind", e.Kind);
        w.WriteString("source", e.Source);
        w.WriteNumber("confidence", e.Confidence);
        if (e.Transform is { } tr)
        {
            w.WritePropertyName("transform");
            w.WriteStartObject();
            if (!string.IsNullOrWhiteSpace(tr.Type)) w.WriteString("type", tr.Type);
            if (!string.IsNullOrWhiteSpace(tr.Method)) w.WriteString("method", tr.Method);
            if (tr.Location is { } loc)
            {
                w.WritePropertyName("location");
                w.WriteStartObject();
                if (!string.IsNullOrWhiteSpace(loc.File)) w.WriteString("file", loc.File);
                w.WriteNumber("line", loc.Line);
                w.WriteEndObject();
            }
            if (tr.MethodSpan is { } ms)
            {
                w.WritePropertyName("method_span");
                w.WriteStartObject();
                w.WriteNumber("start_line", ms.StartLine);
                w.WriteNumber("end_line", ms.EndLine);
                w.WriteEndObject();
            }
            if (tr.IlRange is { } il)
            {
                w.WritePropertyName("il_range");
                w.WriteStartObject();
                if (il.StartOffset is { } so) w.WriteNumber("start_offset", so);
                if (il.EndOffset is { } eo) w.WriteNumber("end_offset", eo);
                w.WriteEndObject();
            }
            w.WriteEndObject();
        }
        if (e.Props is { Count: > 0 })
        {
            w.WritePropertyName("props");
            w.WriteStartObject();
            foreach (var kv in e.Props.OrderBy(k => k.Key, StringComparer.Ordinal))
            {
                WritePropValue(w, kv.Key, kv.Value);
            }
            w.WriteEndObject();
        }
        if (e.Evidence?.Files is { Count: > 0 } files)
        {
            w.WritePropertyName("evidence");
            w.WriteStartObject();
            w.WritePropertyName("files");
            w.WriteStartArray();
            foreach (var f in files)
            {
                w.WriteStartObject();
                if (!string.IsNullOrWhiteSpace(f.Path)) w.WriteString("path", f.Path);
                w.WriteNumber("start_line", f.StartLine);
                w.WriteNumber("end_line", f.EndLine);
                w.WriteEndObject();
            }
            w.WriteEndArray();
            w.WriteEndObject();
        }
        w.WriteEndObject();
    }

    private static void WritePropValue(Utf8JsonWriter w, string key, object? value)
    {
        if (value is null)
        {
            return; // omit nulls
        }
        switch (value)
        {
            case string s:
                w.WriteString(key, s);
                break;
            case int i:
                w.WriteNumber(key, i);
                break;
            case bool b:
                w.WriteBoolean(key, b);
                break;
            case IEnumerable<string> strEnum:
                w.WritePropertyName(key);
                w.WriteStartArray(); foreach (var sv in strEnum) w.WriteStringValue(sv); w.WriteEndArray();
                break;
            case IEnumerable<int> intEnum:
                w.WritePropertyName(key);
                w.WriteStartArray(); foreach (var iv in intEnum) w.WriteNumberValue(iv); w.WriteEndArray();
                break;
            default:
                w.WriteString(key, value.ToString());
                break;
        }
    }

    private async Task WriteGraphCypherAsync(GraphDocument document, CancellationToken cancellationToken)
    {
        var cypherPath = Path.Combine(_outputDirectory, "graph.cypher");
        await using var stream = new FileStream(cypherPath, FileMode.Create, FileAccess.Write, FileShare.None);
        await using var writer = new StreamWriter(stream, Encoding.UTF8);
        var line = new StringBuilder(512);

        foreach (var node in document.Nodes)
        {
            cancellationToken.ThrowIfCancellationRequested();

            line.Clear();
            line.Append("MERGE (n:")
                .Append(node.Type.Replace('.', '_'))
                .Append(" { id: '")
                .Append(node.Id)
                .Append("' })");
            await writer.WriteLineAsync(line.ToString());

            var nodeJson = JsonSerializer.Serialize(node, CypherSerializerOptions);
            line.Clear();
            line.Append("SET n += ").Append(nodeJson);
            await writer.WriteLineAsync(line.ToString());
        }

        foreach (var edge in document.Edges)
        {
            cancellationToken.ThrowIfCancellationRequested();

            line.Clear();
            line.Append("MATCH (a { id: '")
                .Append(edge.From)
                .Append("' }), (b { id: '")
                .Append(edge.To)
                .Append("' })");
            await writer.WriteLineAsync(line.ToString());

            line.Clear();
            line.Append("MERGE (a)-[r:")
                .Append(edge.Kind.Replace('.', '_').ToUpperInvariant())
                .Append(" { id: '")
                .Append(ComputeEdgeId(edge))
                .Append("' }]->(b)");
            await writer.WriteLineAsync(line.ToString());

            var edgeJson = JsonSerializer.Serialize(edge, CypherSerializerOptions);
            line.Clear();
            line.Append("SET r += ").Append(edgeJson);
            await writer.WriteLineAsync(line.ToString());
        }

        await writer.FlushAsync();
    }

    private async Task WriteGraphMarkdownAsync(GraphDocument document, string analyzerVersion, CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# Graph Summary");
        sb.AppendLine();
        sb.AppendLine($"- Analyzer version: {analyzerVersion}");
        sb.AppendLine($"- Nodes: {document.Nodes.Count}");
        sb.AppendLine($"- Edges: {document.Edges.Count}");
        sb.AppendLine();
        sb.AppendLine("## Node Types");
        foreach (var group in document.Nodes.GroupBy(n => n.Type).OrderBy(g => g.Key))
        {
            sb.AppendLine($"- {group.Key}: {group.Count()}");
        }
        sb.AppendLine();
        sb.AppendLine("## Edge Kinds");
        foreach (var group in document.Edges.GroupBy(e => e.Kind).OrderBy(g => g.Key))
        {
            sb.AppendLine($"- {group.Key}: {group.Count()}");
        }

        await File.WriteAllTextAsync(Path.Combine(_outputDirectory, "GRAPH.md"), sb.ToString(), cancellationToken);
    }

    private async Task WriteControllerFlowsAsync(GraphDocument document, CancellationToken cancellationToken)
    {
        var flowDirectory = Path.Combine(_outputDirectory, "flows");
        Directory.CreateDirectory(flowDirectory);

        IGraphProvider provider = new LegacyGraphProvider(document.Nodes, document.Edges);
        var graph = FlowBuilderCore.BuildGraph(provider);
        var endpoints = graph.Nodes
            .Where(static n => n.Type.StartsWith("endpoint.", StringComparison.OrdinalIgnoreCase))
            .OrderBy(static n => n.DisplayName, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (endpoints.Count == 0)
        {
            return;
        }

        var allPath = Path.Combine(flowDirectory, "controllers.all.md");
        await using var stream = new FileStream(allPath, FileMode.Create, FileAccess.Write, FileShare.None);
        await using var writer = new StreamWriter(stream);

        foreach (var endpoint in endpoints)
        {
            var flow = FlowBuilderMarkdown.Render(graph, node => string.Equals(node.Id, endpoint.Id, StringComparison.Ordinal));
            if (string.IsNullOrWhiteSpace(flow))
            {
                continue;
            }

            await writer.WriteLineAsync(flow);
            if (!flow.EndsWith(Environment.NewLine, StringComparison.Ordinal))
            {
                await writer.WriteLineAsync();
            }

            var basis = !string.IsNullOrWhiteSpace(endpoint.DisplayName) ? endpoint.DisplayName : endpoint.Id;
            if (string.IsNullOrWhiteSpace(basis))
            {
                continue;
            }

            var fileName = SanitizeFileName(basis) + ".md";
            await File.WriteAllTextAsync(Path.Combine(flowDirectory, fileName), flow, cancellationToken);
        }

        await writer.FlushAsync();
    }

    private async Task WriteVersionAsync(string analyzerVersion, CancellationToken cancellationToken)
    {
        var gitSha = TryGetGitCommit() ?? "unknown";
        var text = $"AnalyzerVersion={analyzerVersion}\nGitCommit={gitSha}\n";
        await File.WriteAllTextAsync(Path.Combine(_outputDirectory, "VERSION.txt"), text, cancellationToken);
    }

    private static string? TryGetGitCommit()
    {
        try
        {
            var startInfo = new ProcessStartInfo("git", "rev-parse HEAD")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var process = Process.Start(startInfo);
            process?.WaitForExit(2000);
            return process?.StandardOutput.ReadLine();
        }
        catch
        {
            return null;
        }
    }

    private async Task WriteEvalAsync(GraphDocument document, CancellationToken cancellationToken)
    {
        var evalDir = Path.Combine(_outputDirectory, "evals");
        Directory.CreateDirectory(evalDir);
        var controllerCount = document.Nodes.Count(n => string.Equals(n.Type, "endpoint.controller", StringComparison.Ordinal));
        var payload = new
        {
            name = "controller-flows",
            status = controllerCount > 0 ? "informational" : "warning",
            controllers = controllerCount,
            nodes = document.Nodes.Count,
            edges = document.Edges.Count
        };
        await File.WriteAllTextAsync(Path.Combine(evalDir, "controllers.json"), JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true }), cancellationToken);
    }

    private static string SanitizeFileName(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        var builder = new StringBuilder(name.Length);

        foreach (var ch in name)
        {
            if (ch == '.' || ch == ':' || ch == ' ' || Array.IndexOf(invalid, ch) >= 0)
            {
                builder.Append('_');
            }
            else
            {
                builder.Append(ch);
            }
        }

        return builder.ToString();
    }

    private static string? GetPropValue(GraphNode node, string key)
        => node.Props is { } props && props.TryGetValue(key, out var value) ? value?.ToString() : null;

    private static string ComputeEdgeId(GraphEdge edge)
    {
        using var sha = SHA256.Create();
        var builder = new StringBuilder();
        builder.Append(edge.From)
            .Append('|').Append(edge.To)
            .Append('|').Append(edge.Kind)
            .Append('|').Append(edge.Source)
            .Append('|').Append(edge.Confidence.ToString("F3", System.Globalization.CultureInfo.InvariantCulture));

        if (edge.Transform is { } transform)
        {
            builder.Append('|').Append(transform.Type ?? string.Empty)
                .Append('|').Append(transform.Method ?? string.Empty);
            if (transform.Location is { } location)
            {
                builder.Append('|').Append(location.File).Append(':').Append(location.Line);
            }

            if (transform.MethodSpan is { } span)
            {
                builder.Append('|').Append(span.StartLine).Append('-').Append(span.EndLine);
            }

            if (transform.IlRange is { } il)
            {
                builder.Append('|').Append(il.StartOffset?.ToString() ?? string.Empty)
                    .Append('-').Append(il.EndOffset?.ToString() ?? string.Empty);
            }
        }

        builder.Append('|').Append(FormatProps(edge.Props));
        builder.Append('|').Append(FormatEvidence(edge.Evidence));

        var payload = Encoding.UTF8.GetBytes(builder.ToString());
        var hash = sha.ComputeHash(payload);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    private static string FormatProps(IReadOnlyDictionary<string, object>? props)
    {
        if (props is null || props.Count == 0)
        {
            return string.Empty;
        }

        return string.Join('|', props
            .OrderBy(p => p.Key, StringComparer.Ordinal)
            .Select(p => $"{p.Key}={p.Value}"));
    }

    private static string FormatEvidence(GraphEvidence? evidence)
    {
        if (evidence?.Files is not { Count: > 0 } files)
        {
            return string.Empty;
        }

        return string.Join('|', files
            .OrderBy(f => f.Path, StringComparer.Ordinal)
            .ThenBy(f => f.StartLine)
            .Select(f => $"{f.Path}:{f.StartLine}-{f.EndLine}"));
    }
}
