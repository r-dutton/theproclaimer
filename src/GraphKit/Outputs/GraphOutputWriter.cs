
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Linq;
using GraphKit.Graph;

namespace GraphKit.Outputs;

public sealed class GraphOutputWriter
{
    private readonly string _workspaceRoot;
    private readonly string _outputDirectory;

    public GraphOutputWriter(string workspaceRoot, string outputDirectory)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        _outputDirectory = Path.GetFullPath(Path.Combine(_workspaceRoot, outputDirectory));
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
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        await using var stream = File.Create(jsonPath);
        await JsonSerializer.SerializeAsync(stream, document, options, cancellationToken);
    }

    private async Task WriteGraphCypherAsync(GraphDocument document, CancellationToken cancellationToken)
    {
        var builder = new StringBuilder();
        foreach (var node in document.Nodes)
        {
            builder.Append("MERGE (n:" + node.Type.Replace('.', '_') + " { id: '" + node.Id + "' })\n");
            builder.Append("SET n += " + JsonSerializer.Serialize(node) + "\n");
        }

        foreach (var edge in document.Edges)
        {
            builder.Append("MATCH (a { id: '" + edge.From + "' }), (b { id: '" + edge.To + "' })\n");
            builder.Append("MERGE (a)-[r:" + edge.Kind.Replace('.', '_').ToUpperInvariant() + " { id: '" + ComputeEdgeId(edge) + "' }]->(b)\n");
            builder.Append("SET r += " + JsonSerializer.Serialize(edge) + "\n");
        }

        await File.WriteAllTextAsync(Path.Combine(_outputDirectory, "graph.cypher"), builder.ToString(), cancellationToken);
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

        var controllers = document.Nodes
            .Where(n => string.Equals(n.Type, "endpoint.controller", StringComparison.Ordinal))
            .OrderBy(n => n.Fqdn, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (controllers.Count == 0)
        {
            return;
        }

        var allFlows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "*" }));
        if (!string.IsNullOrWhiteSpace(allFlows))
        {
            await File.WriteAllTextAsync(Path.Combine(flowDirectory, "controllers.all.md"), allFlows, cancellationToken);
        }

        foreach (var controller in controllers)
        {
            var flow = FlowBuilder.BuildFlows(document, node => string.Equals(node.Id, controller.Id, StringComparison.Ordinal));
            if (string.IsNullOrWhiteSpace(flow))
            {
                continue;
            }

            var fileName = SanitizeFileName(controller.Id) + ".md";
            await File.WriteAllTextAsync(Path.Combine(flowDirectory, fileName), flow, cancellationToken);
        }
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
