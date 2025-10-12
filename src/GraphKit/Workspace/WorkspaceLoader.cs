using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GraphKit.Workspace;

public sealed class WorkspaceLoader
{
    private static readonly Regex ProjectLine = new(
        @"^Project\(""{[^""]+}""\) = ""(?<name>[^""]+)"", ""(?<path>[^""]+)"",");

    public WorkspaceModel Load(string rootPath)
    {
        var workspaceConfig = Path.Combine(rootPath, "flow.workspace.json");
        List<SolutionModel> solutions;
        if (File.Exists(workspaceConfig))
        {
            var json = File.ReadAllText(workspaceConfig);
            var document = JsonSerializer.Deserialize<WorkspaceConfig>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            solutions = document?.Solutions?.Select(s => ResolveSolution(rootPath, s)).Where(s => s != null).Cast<SolutionModel>().ToList()
                ?? new List<SolutionModel>();
        }
        else
        {
            solutions = Directory.EnumerateFiles(rootPath, "*.sln", SearchOption.AllDirectories)
                .Where(path => !path.Contains("/bin/") && !path.Contains("/obj/") && !path.Contains("/.git/"))
                .Select(path => new SolutionReference
                {
                    Name = Path.GetFileNameWithoutExtension(path),
                    Path = Path.GetRelativePath(rootPath, path)
                })
                .Select(s => ResolveSolution(rootPath, s))
                .Where(s => s != null)
                .Cast<SolutionModel>()
                .ToList();
        }

        return new WorkspaceModel
        {
            RootPath = rootPath,
            Solutions = solutions
        };
    }

    private SolutionModel? ResolveSolution(string rootPath, SolutionReference reference)
    {
        var solutionPath = Path.GetFullPath(Path.Combine(rootPath, reference.Path));
        if (!File.Exists(solutionPath))
        {
            return null;
        }

        var solutionDir = Path.GetDirectoryName(solutionPath) ?? rootPath;
        var projects = new List<ProjectModel>();
        foreach (var line in File.ReadLines(solutionPath))
        {
            var match = ProjectLine.Match(line);
            if (!match.Success)
            {
                continue;
            }

            var projectPath = match.Groups["path"].Value.Replace("\\", Path.DirectorySeparatorChar.ToString());
            if (!projectPath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var fullProjectPath = Path.GetFullPath(Path.Combine(solutionDir, projectPath));
            if (!File.Exists(fullProjectPath))
            {
                continue;
            }

            projects.Add(new ProjectModel
            {
                Name = Path.GetFileNameWithoutExtension(fullProjectPath),
                RelativePath = Path.GetRelativePath(rootPath, fullProjectPath),
                FullPath = fullProjectPath
            });
        }

        return new SolutionModel
        {
            Name = reference.Name ?? Path.GetFileNameWithoutExtension(solutionPath),
            RelativePath = Path.GetRelativePath(rootPath, solutionPath),
            FullPath = solutionPath,
            Projects = projects
        };
    }

    private sealed class WorkspaceConfig
    {
        public List<SolutionReference>? Solutions { get; set; }
    }

    private sealed class SolutionReference
    {
        public string? Name { get; set; }

        public string Path { get; set; } = string.Empty;
    }
}

public sealed class WorkspaceModel
{
    public string RootPath { get; init; } = string.Empty;

    public List<SolutionModel> Solutions { get; init; } = new();
}

public sealed class SolutionModel
{
    public string Name { get; init; } = string.Empty;

    public string RelativePath { get; init; } = string.Empty;

    public string FullPath { get; init; } = string.Empty;

    public List<ProjectModel> Projects { get; init; } = new();
}

public sealed class ProjectModel
{
    public string Name { get; init; } = string.Empty;

    public string RelativePath { get; init; } = string.Empty;

    public string FullPath { get; init; } = string.Empty;

    public string Directory => Path.GetDirectoryName(FullPath) ?? string.Empty;
}
