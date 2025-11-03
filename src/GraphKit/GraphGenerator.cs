using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphKit.Analyzers;
using GraphKit.Constants;
using GraphKit.Facts;
using GraphKit.Graph;
using GraphKit.Outputs;
using GraphKit.Outputs.Facts;
using GraphKit.Workspace;
using System.Text.Json;

namespace GraphKit;

public sealed class GraphGenerator
{
    private const string AnalyzerVersion = "0.2.3";

    public async Task<GraphGenerationResult> GenerateAsync(GraphGenerationOptions options, CancellationToken cancellationToken = default)
    {
        var loader = new WorkspaceLoader(
            options.WorkspacePath,
            options.Solutions,
            options.UseRoslyn,
            options.RoslynWorkspace);
        var loadResult = await loader.LoadAsync(cancellationToken); // process all solutions/projects without filtering
        var projects = loadResult.Projects;
        var roslynProjects = loadResult.RoslynProjects;

        static string GetLookupKey(ProjectInfo projectInfo)
            => string.IsNullOrWhiteSpace(projectInfo.ProjectPath)
                ? projectInfo.AssemblyName
                : projectInfo.ProjectPath;

        var roslynLookup = roslynProjects.Count > 0
            ? roslynProjects.ToDictionary(static entry => GetLookupKey(entry.Project), static entry => entry, StringComparer.OrdinalIgnoreCase)
            : null;

        Console.WriteLine($"[graph] Loaded {projects.Count} projects. Memory={GC.GetTotalMemory(false)/1024/1024:F1}MB");

        var fingerprints = await ProjectFingerprint.ComputeAsync(projects, options.WorkspacePath, cancellationToken);
        var cacheManager = new WorkspaceCacheManager(options.WorkspacePath, options.OutputDirectory);
        var outputWriter = new GraphOutputWriter(options.WorkspacePath, options.OutputDirectory);
        var cachedDocument = await cacheManager.TryLoadCachedDocumentAsync(AnalyzerVersion, fingerprints, cancellationToken);
        if (cachedDocument is not null)
        {
            Console.WriteLine("[graph] Cache hit. Skipping analysis and reusing existing graph.");
            var cachedFacts = new FactWriter();
            PopulateFactsFromGraph(cachedDocument, cachedFacts, options.WorkspacePath);
            var cachedBag = FactsPipeline.Finalize(cachedFacts);
            var cacheFactsDirectory = Path.GetFullPath(Path.Combine(options.WorkspacePath, options.OutputDirectory));
            FactsJsonWriter.Write(cachedBag, Path.Combine(cacheFactsDirectory, "facts.json"));
            await outputWriter.WriteAsync(cachedDocument, cachedBag, AnalyzerVersion, cancellationToken);

            return new GraphGenerationResult(cachedDocument, cachedBag);
        }

        var factWriter = new FactWriter();
        var analyzer = new ProjectAnalyzer(options.WorkspacePath, factWriter, options.AnalyzerConfiguration);
        await Parallel.ForEachAsync(projects, cancellationToken, async (project, ct) =>
        {
            var projectKey = GetLookupKey(project);
            roslynLookup?.TryGetValue(projectKey, out var roslynProject);
            await analyzer.AnalyzeProjectAsync(project, roslynProject, ct);
            Console.WriteLine($"[graph] Analyzed project {project.AssemblyName} ({project.SourceFiles.Count} files). Nodes={analyzer.NodeCount} Edges={analyzer.EdgeCount} Mem={GC.GetTotalMemory(false) / 1024 / 1024:F1}MB");
        });

        var document = analyzer.BuildDocument(AnalyzerVersion);
    Console.WriteLine($"[graph] Built document. Nodes={document.Nodes.Count} Edges={document.Edges.Count} Mem={GC.GetTotalMemory(false)/1024/1024:F1}MB");

        PopulateFactsFromGraph(document, factWriter, options.WorkspacePath);

        var factBag = FactsPipeline.Finalize(factWriter);
        var factsOutputDirectory = Path.GetFullPath(Path.Combine(options.WorkspacePath, options.OutputDirectory));
        var factsPath = Path.Combine(factsOutputDirectory, "facts.json");
        Console.WriteLine($"[facts] Collected Nodes={factBag.Nodes.Count} Edges={factBag.Edges.Count}");
        FactsJsonWriter.Write(factBag, factsPath);

        await outputWriter.WriteAsync(document, factBag, AnalyzerVersion, cancellationToken);
        await cacheManager.SaveAsync(AnalyzerVersion, document, fingerprints, cancellationToken);

        return new GraphGenerationResult(document, factBag);
    }

    private static string MapConfidence(double value)
        => value >= 0.9 ? "High" : value >= 0.6 ? "Medium" : "Low";

    private static string MapProvenance(string source) => source switch
    {
        "static" => "Static",
        "synthetic" => "Heuristic",
        "runtime" => "Interprocedural",
        _ => "Unknown"
    };

    private static void PopulateFactsFromGraph(GraphDocument document, FactWriter facts, string workspaceRoot)
    {
        var workspaceFullPath = Path.GetFullPath(workspaceRoot);

        foreach (var node in document.Nodes)
        {
            var props = new Dictionary<string, object?>
            {
                ["name"] = node.Name,
                ["fqdn"] = node.Fqdn,
                ["assembly"] = node.Assembly,
                ["project"] = node.Project,
                ["file_path"] = node.FilePath,
                ["symbol_id"] = node.SymbolId
            };

            if (node.Tags is { Count: > 0 })
            {
                props["tags"] = node.Tags.ToArray();
            }

            if (node.Span is { } span)
            {
                props["span"] = new Dictionary<string, object?>
                {
                    ["start_line"] = span.StartLine,
                    ["end_line"] = span.EndLine
                };
            }

            if (node.Props is { Count: > 0 })
            {
                foreach (var kv in node.Props)
                {
                    props[kv.Key] = NormalizeJsonValue(kv.Value);
                }
            }

            var normalizedFile = NormalizeToWorkspace(workspaceFullPath, node.FilePath);
            props.AddSource(normalizedFile, node.Span);

            if (!props.ContainsKey(PropKeys.Verb) && props.TryGetValue("http_method", out var httpMethod) && httpMethod is string methodValue && !string.IsNullOrWhiteSpace(methodValue))
            {
                props[PropKeys.Verb] = methodValue;
            }

            if (!props.ContainsKey("controller_display"))
            {
                var display = !string.IsNullOrWhiteSpace(node.Fqdn) ? node.Fqdn : node.Name;
                if (!string.IsNullOrWhiteSpace(display))
                {
                    props["controller_display"] = display!;
                }
            }

            if (!props.ContainsKey("auth"))
            {
                var authLabel = InferAuthLabel(props);
                if (!string.IsNullOrWhiteSpace(authLabel))
                {
                    props["auth"] = authLabel!;
                }
            }

            facts.AddNode(new NodeFact(node.Id, node.Type, props));
        }

        foreach (var edge in document.Edges)
        {
            var props = new Dictionary<string, object?>
            {
                ["provenance"] = MapProvenance(edge.Source),
                ["confidence"] = MapConfidence(edge.Confidence)
            };

            if (edge.Transform?.Type is { } transformType && !string.IsNullOrWhiteSpace(transformType))
            {
                props["transform_type"] = transformType;
            }

            if (edge.Transform?.Method is { } transformMethod && !string.IsNullOrWhiteSpace(transformMethod))
            {
                props["transform_method"] = transformMethod;
            }

            if (edge.Transform?.Location is { } loc)
            {
                props["line"] = loc.Line;
            }

            if (edge.Props is { Count: > 0 })
            {
                foreach (var kv in edge.Props)
                {
                    props[kv.Key] = kv.Value;
                }
            }

            if (ResolveEdgeSource(workspaceFullPath, edge) is { } source)
            {
                props.AddSource(source.File, source.StartLine, source.EndLine);
                if (!props.ContainsKey("line"))
                {
                    props["line"] = source.StartLine;
                }
            }

            facts.AddEdge(new EdgeFact(edge.From, edge.To, edge.Kind, props));
        }
    }

    private static (string File, int StartLine, int EndLine)? ResolveEdgeSource(string workspaceRoot, GraphEdge edge)
    {
        if (edge.Evidence?.Files is { Count: > 0 } evidenceFiles)
        {
            var primary = evidenceFiles[0];
            var file = NormalizeToWorkspace(workspaceRoot, primary.Path);
            if (!string.IsNullOrWhiteSpace(file))
            {
                return (file!, primary.StartLine, primary.EndLine);
            }
        }

        if (edge.Transform?.Location is { } loc)
        {
            var file = NormalizeToWorkspace(workspaceRoot, loc.File);
            if (!string.IsNullOrWhiteSpace(file))
            {
                if (edge.Transform.MethodSpan is { } span)
                {
                    return (file!, span.StartLine, span.EndLine);
                }

                return (file!, loc.Line, loc.Line);
            }
        }

        return null;
    }

    private static object? NormalizeJsonValue(object? value)
        => value is JsonElement element ? NormalizeJsonElement(element) : value;

    private static object? NormalizeJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Null:
                return null;
            case JsonValueKind.String:
                return element.GetString();
            case JsonValueKind.Number:
                if (element.TryGetInt64(out var l))
                {
                    return l;
                }

                if (element.TryGetDouble(out var d))
                {
                    return d;
                }

                return element.GetRawText();
            case JsonValueKind.True:
            case JsonValueKind.False:
                return element.GetBoolean();
            case JsonValueKind.Array:
            {
                var list = new List<object?>();
                foreach (var item in element.EnumerateArray())
                {
                    list.Add(NormalizeJsonElement(item));
                }

                return list;
            }
            case JsonValueKind.Object:
            {
                var dict = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                foreach (var prop in element.EnumerateObject())
                {
                    dict[prop.Name] = NormalizeJsonElement(prop.Value);
                }

                return dict;
            }
            default:
                return element.GetRawText();
        }
    }

    private static string? NormalizeToWorkspace(string workspaceRoot, string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        var absolute = Path.IsPathRooted(path)
            ? Path.GetFullPath(path)
            : Path.GetFullPath(Path.Combine(workspaceRoot, path));

        if (!absolute.StartsWith(workspaceRoot, StringComparison.OrdinalIgnoreCase))
        {
            return absolute.Replace('\\', '/');
        }

        var relative = Path.GetRelativePath(workspaceRoot, absolute);
        return relative.Replace('\\', '/');
    }

    private static string? InferAuthLabel(Dictionary<string, object?> props)
    {
        if (props.TryGetValue("auth", out var existing) && existing is string authValue && !string.IsNullOrWhiteSpace(authValue))
        {
            return authValue;
        }

        if (props.TryGetValue("allow_anonymous", out var allowObj) && allowObj is bool allow && allow)
        {
            return "anonymous";
        }

        if (props.TryGetValue("authorization", out var authorizationObj) && authorizationObj is IEnumerable<object?> entries)
        {
            string? Extract(object? entry)
            {
                return entry switch
                {
                    IReadOnlyDictionary<string, object?> readOnlyDict => FirstNonEmpty(readOnlyDict, "policy", "roles", "authentication_schemes"),
                    IDictionary<string, object> dict => FirstNonEmpty(dict.Select(kv => new KeyValuePair<string, object?>(kv.Key, kv.Value)), "policy", "roles", "authentication_schemes"),
                    _ => null
                };
            }

            foreach (var entry in entries)
            {
                var value = Extract(entry);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            if (entries.Any())
            {
                return "user";
            }
        }

        return null;
    }

    private static string? FirstNonEmpty(IEnumerable<KeyValuePair<string, object?>> entries, params string[] keys)
    {
        foreach (var key in keys)
        {
            foreach (var pair in entries)
            {
                if (!string.Equals(pair.Key, key, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (pair.Value is string text && !string.IsNullOrWhiteSpace(text))
                {
                    return text;
                }
            }
        }

        return null;
    }
}
