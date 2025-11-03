using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace GraphKit.Workspace;

public sealed class RoslynWorkspaceLoader
{
    private readonly string _workspaceRoot;
    private readonly MSBuildWorkspace _workspace;
    private readonly IReadOnlyList<string> _solutionPaths;

    public RoslynWorkspaceLoader(string workspaceRoot, MSBuildWorkspace workspace, IReadOnlyList<string> solutionPaths)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        _workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        _solutionPaths = solutionPaths ?? throw new ArgumentNullException(nameof(solutionPaths));
    }

    public async Task<IReadOnlyList<ProjectInfo>> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (_solutionPaths.Count == 0)
        {
            return Array.Empty<ProjectInfo>();
        }

        var projects = new Dictionary<string, ProjectInfo>(StringComparer.OrdinalIgnoreCase);
        var processedSolutions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var solutionPath in _solutionPaths)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(solutionPath) || !processedSolutions.Add(solutionPath))
            {
                continue;
            }

            if (!File.Exists(solutionPath))
            {
                continue;
            }

            if (_workspace.CurrentSolution.ProjectIds.Count > 0)
            {
                _workspace.CloseSolution();
            }

            var solution = await _workspace.OpenSolutionAsync(solutionPath, cancellationToken: cancellationToken).ConfigureAwait(false);

            foreach (var project in solution.Projects)
            {
                if (!string.Equals(project.Language, LanguageNames.CSharp, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var compilation = await project.GetCompilationAsync(cancellationToken).ConfigureAwait(false);
                if (compilation is null)
                {
                    continue;
                }

                var documentEntries = new List<(Document Document, VersionStamp Version)>();
                foreach (var document in project.Documents)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (!IsCSharpDocument(document))
                    {
                        continue;
                    }

                    var version = await document.GetTextVersionAsync(cancellationToken).ConfigureAwait(false);
                    documentEntries.Add((document, version));
                }

                if (documentEntries.Count == 0)
                {
                    continue;
                }

                var key = ResolveProjectKey(project);
                if (projects.ContainsKey(key))
                {
                    continue;
                }

                var documentIds = documentEntries
                    .Select(entry => entry.Document.Id)
                    .ToImmutableArray();

                var documentFilePaths = documentEntries
                    .Where(entry => !string.IsNullOrWhiteSpace(entry.Document.FilePath))
                    .ToImmutableDictionary(
                        entry => entry.Document.Id,
                        entry => Path.GetFullPath(entry.Document.FilePath!),
                        DocumentIdComparer.Instance);

                var documentVersions = documentEntries
                    .ToImmutableDictionary(
                        entry => entry.Document.Id,
                        entry => entry.Version,
                        DocumentIdComparer.Instance);

                var sourceFiles = documentFilePaths.Values
                    .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var projectPath = project.FilePath ?? key;
                var relativeDir = ResolveRelativeDirectory(projectPath);
                var assemblyName = project.AssemblyName ?? project.Name;
                var rootNamespace = project.DefaultNamespace ?? assemblyName;

                projects[key] = new ProjectInfo(
                    projectPath,
                    assemblyName,
                    rootNamespace,
                    relativeDir,
                    sourceFiles,
                    () => compilation,
                    project.Id,
                    documentIds,
                    documentFilePaths,
                    documentVersions,
                    project.Version);
            }
        }

        if (_workspace.CurrentSolution.ProjectIds.Count > 0)
        {
            _workspace.CloseSolution();
        }

        return projects
            .Values
            .OrderBy(p => p.ProjectPath, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private string ResolveRelativeDirectory(string projectPath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(projectPath) || string.IsNullOrWhiteSpace(_workspaceRoot))
            {
                return string.Empty;
            }

            if (!Path.IsPathRooted(projectPath))
            {
                projectPath = Path.Combine(_workspaceRoot, projectPath);
            }

            var directory = Path.GetDirectoryName(projectPath);
            if (string.IsNullOrWhiteSpace(directory))
            {
                return string.Empty;
            }

            return Path.GetRelativePath(_workspaceRoot, directory).Replace('\\', '/');
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    private static string ResolveProjectKey(Project project)
    {
        if (!string.IsNullOrWhiteSpace(project.FilePath))
        {
            return Path.GetFullPath(project.FilePath);
        }

        return project.Id.Id.ToString();
    }

    private static bool IsCSharpDocument(Document document)
    {
        if (!string.Equals(document.Language, LanguageNames.CSharp, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (document.SourceCodeKind != SourceCodeKind.Regular)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(document.FilePath))
        {
            return false;
        }

        return Path.GetExtension(document.FilePath).Equals(".cs", StringComparison.OrdinalIgnoreCase);
    }

    private sealed class DocumentIdComparer : IEqualityComparer<DocumentId>
    {
        public static readonly DocumentIdComparer Instance = new();

        public bool Equals(DocumentId? x, DocumentId? y)
        {
            if (x is null || y is null)
            {
                return x is null && y is null;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(DocumentId obj) => obj.Id.GetHashCode();
    }
}
