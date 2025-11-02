using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.Linq;
using System.Threading.Tasks;

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

        if (projectPaths.Count == 0)
        {
            return Array.Empty<ProjectInfo>();
        }

        var bag = new ConcurrentBag<ProjectInfo>();
        var parallelOptions = new ParallelOptions
        {
            CancellationToken = cancellationToken,
            MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount - 1)
        };

        Parallel.ForEach(projectPaths, parallelOptions, projectPath =>
        {
            parallelOptions.CancellationToken.ThrowIfCancellationRequested();

            var projectDir = Path.GetDirectoryName(projectPath)!;
            var relativeDir = Path.GetRelativePath(_workspaceRoot, projectDir).Replace('\\', '/');
            using var stream = File.OpenRead(projectPath);
            var projectXml = XDocument.Load(stream);
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

            var files = EnumerateSourceFiles(projectDir)
                .Select(Path.GetFullPath)
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToList();

            bag.Add(new ProjectInfo(
                projectPath,
                assemblyName,
                rootNamespace,
                relativeDir,
                files));
        });

        return bag
            .OrderBy(p => p.ProjectPath, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static IEnumerable<string> EnumerateSourceFiles(string projectDirectory)
    {
        var stack = new Stack<string>();
        stack.Push(projectDirectory);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(current, "*.cs", SearchOption.TopDirectoryOnly);
            }
            catch (IOException)
            {
                continue;
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            foreach (var file in files)
            {
                yield return file;
            }

            IEnumerable<string> directories;
            try
            {
                directories = Directory.EnumerateDirectories(current);
            }
            catch (IOException)
            {
                continue;
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            foreach (var directory in directories)
            {
                var name = Path.GetFileName(directory);
                if (IsIgnoredDirectory(name))
                {
                    continue;
                }

                stack.Push(directory);
            }
        }
    }

    private static bool IsIgnoredDirectory(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return name.Equals("bin", StringComparison.OrdinalIgnoreCase) ||
               name.Equals("obj", StringComparison.OrdinalIgnoreCase) ||
               name.Equals(".git", StringComparison.OrdinalIgnoreCase) ||
               name.Equals(".vs", StringComparison.OrdinalIgnoreCase) ||
               name.Equals("node_modules", StringComparison.OrdinalIgnoreCase);
    }
}
