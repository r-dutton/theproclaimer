using System.Text.Json;
using System.Xml.Linq;
using System.Linq;

namespace GraphKit.Workspace;

public sealed class WorkspaceLoader
{
    private readonly string _workspaceRoot;
    private readonly IReadOnlyList<string>? _explicitSolutions;

    public WorkspaceLoader(string workspaceRoot, IReadOnlyList<string>? explicitSolutions = null)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        _explicitSolutions = explicitSolutions;
    }

    public async Task<IReadOnlyList<ProjectInfo>> LoadAsync(CancellationToken cancellationToken = default)
    {
        var configPath = Path.Combine(_workspaceRoot, "flow.workspace.json");
        var solutionPaths = new List<string>();

        if (_explicitSolutions is { Count: > 0 })
        {
            foreach (var candidate in _explicitSolutions)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    continue;
                }

                var path = Path.GetFullPath(Path.IsPathRooted(candidate)
                    ? candidate
                    : Path.Combine(_workspaceRoot, candidate));

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Solution '{candidate}' could not be resolved relative to '{_workspaceRoot}'.", path);
                }

                solutionPaths.Add(path);
            }
        }
        else if (File.Exists(configPath))
        {
            using var stream = File.OpenRead(configPath);
            var doc = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
            if (doc.RootElement.TryGetProperty("solutions", out var solutionsElement))
            {
                foreach (var element in solutionsElement.EnumerateArray())
                {
                    var rel = element.GetString();
                    if (!string.IsNullOrWhiteSpace(rel))
                    {
                        solutionPaths.Add(Path.GetFullPath(Path.Combine(_workspaceRoot, rel)));
                    }
                }
            }
        }
        else
        {
            solutionPaths.AddRange(Directory
                .EnumerateFiles(_workspaceRoot, "*.sln", SearchOption.AllDirectories)
                .Where(path => !path.Contains("/bin/") && !path.Contains("/obj/") && !path.Contains("/.git/")));
        }

        var projectPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var solutionPath in solutionPaths.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            foreach (var line in await File.ReadAllLinesAsync(solutionPath, cancellationToken))
            {
                if (!line.StartsWith("Project", StringComparison.Ordinal))
                {
                    continue;
                }

                var parts = line.Split('"');
                if (parts.Length >= 6)
                {
                    var projectRelative = parts[5].Replace('\\', Path.DirectorySeparatorChar);
                    var candidate = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(solutionPath)!, projectRelative));
                    if (candidate.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
                    {
                        projectPaths.Add(candidate);
                    }
                }
            }
        }

        var results = new List<ProjectInfo>();
        foreach (var projectPath in projectPaths.OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
        {
            var projectDir = Path.GetDirectoryName(projectPath)!;
            var relativeDir = Path.GetRelativePath(_workspaceRoot, projectDir).Replace('\\', '/');
            var projectXml = XDocument.Load(projectPath);
            var ns = projectXml.Root?.Name.Namespace ?? XNamespace.None;
            var assemblyName = projectXml.Root?
                .Elements(ns + "PropertyGroup")
                .Elements(ns + "AssemblyName")
                .Select(e => e.Value)
                .FirstOrDefault() ?? Path.GetFileNameWithoutExtension(projectPath);

            var rootNamespace = projectXml.Root?
                .Elements(ns + "PropertyGroup")
                .Elements(ns + "RootNamespace")
                .Select(e => e.Value)
                .FirstOrDefault() ?? assemblyName;

            var files = Directory
                .EnumerateFiles(projectDir, "*.cs", SearchOption.AllDirectories)
                .Where(path => !path.Contains("/bin/") && !path.Contains("/obj/"))
                .Select(path => Path.GetFullPath(path))
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToList();

            results.Add(new ProjectInfo(
                projectPath,
                assemblyName,
                rootNamespace,
                relativeDir,
                files));
        }

        return results;
    }
}
