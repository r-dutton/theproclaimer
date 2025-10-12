using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using GraphKit.Graph;

namespace GraphKit.Output;

public sealed class GraphWriter
{
    public void Write(GraphDocument document, string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);
        WriteGraphJson(document, Path.Combine(outputDirectory, "graph.json"));
        WriteGraphCypher(document, Path.Combine(outputDirectory, "graph.cypher"));
        WriteGraphMarkdown(document, Path.Combine(outputDirectory, "GRAPH.md"));
        WriteVersion(document, outputDirectory);
    }

    private static void WriteGraphJson(GraphDocument document, string path)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(document, options);
        File.WriteAllText(path, json);
    }

    private static void WriteGraphCypher(GraphDocument document, string path)
    {
        var builder = new StringBuilder();
        foreach (var node in document.Nodes)
        {
            builder.Append("MERGE (n");
            builder.Append(node.Id.Replace("node::", "_"));
            builder.Append(":");
            builder.Append(node.Type.Replace('.', '_'));
            builder.Append(" { id: \"");
            builder.Append(node.Id);
            builder.Append("\" }");
            builder.AppendLine(")");
        }

        foreach (var edge in document.Edges)
        {
            builder.Append("MATCH (a { id: \"");
            builder.Append(edge.From);
            builder.Append("\" }), (b { id: \"");
            builder.Append(edge.To);
            builder.Append("\" }) MERGE (a)-[:");
            builder.Append(edge.Kind.Replace('.', '_'));
            builder.Append(" { source: \"");
            builder.Append(edge.Source);
            builder.Append("\", confidence: ");
            builder.Append(edge.Confidence.ToString("0.###"));
            builder.Append(" }]->(b)");
            builder.AppendLine();
        }

        File.WriteAllText(path, builder.ToString());
    }

    private static void WriteGraphMarkdown(GraphDocument document, string path)
    {
        var webNodes = document.Nodes.Count(n => n.Tags.Contains("web"));
        var dataNodes = document.Nodes.Count(n => n.Tags.Contains("db") || n.Tags.Contains("data"));
        var msgNodes = document.Nodes.Count(n => n.Tags.Contains("messaging"));

        var builder = new StringBuilder();
        builder.AppendLine("# Graph Summary");
        builder.AppendLine();
        builder.AppendLine($"* Nodes: {document.Nodes.Count}");
        builder.AppendLine($"* Edges: {document.Edges.Count}");
        builder.AppendLine($"* Web Nodes: {webNodes}");
        builder.AppendLine($"* Data Nodes: {dataNodes}");
        builder.AppendLine($"* Messaging Nodes: {msgNodes}");
        File.WriteAllText(path, builder.ToString());
    }

    private static void WriteVersion(GraphDocument document, string outputDirectory)
    {
        var versionPath = Path.Combine(outputDirectory, "VERSION.txt");
        var gitHash = TryGetGitCommit();
        var text = new StringBuilder();
        text.AppendLine(document.AnalyzerVersion);
        if (!string.IsNullOrEmpty(gitHash))
        {
            text.AppendLine(gitHash);
        }

        File.WriteAllText(versionPath, text.ToString());
    }

    private static string? TryGetGitCommit()
    {
        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo("git", "rev-parse HEAD")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            using var process = System.Diagnostics.Process.Start(psi);
            process?.WaitForExit(2000);
            var output = process?.StandardOutput.ReadToEnd().Trim();
            return string.IsNullOrEmpty(output) ? null : output;
        }
        catch
        {
            return null;
        }
    }
}
