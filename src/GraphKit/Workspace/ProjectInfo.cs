using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace GraphKit.Workspace;

public sealed class ProjectInfo
{
    private readonly Lazy<Compilation> _compilationFactory;
    private readonly ConcurrentDictionary<SyntaxTree, SemanticModel> _semanticModels = new();
    private ImmutableDictionary<string, SyntaxTree>? _syntaxTreeCache;
    private readonly object _syntaxTreeLock = new();
    private readonly bool _isRoslyn;
    private readonly ProjectId? _projectId;
    private readonly ImmutableArray<DocumentId> _documentIds;
    private readonly ImmutableDictionary<DocumentId, string> _documentFilePaths;

    internal ImmutableDictionary<DocumentId, VersionStamp> DocumentVersions { get; }
    internal VersionStamp? RoslynProjectVersion { get; }

    public ProjectInfo(
        string projectPath,
        string assemblyName,
        string rootNamespace,
        string relativeDirectory,
        IReadOnlyList<string> sourceFiles,
        Func<Compilation> compilationFactory)
        : this(
            projectPath,
            assemblyName,
            rootNamespace,
            relativeDirectory,
            sourceFiles,
            compilationFactory,
            isRoslyn: false,
            projectId: null,
            documentIds: ImmutableArray<DocumentId>.Empty,
            documentFilePaths: ImmutableDictionary<DocumentId, string>.Empty,
            documentVersions: ImmutableDictionary<DocumentId, VersionStamp>.Empty,
            projectVersion: null)
    {
    }

    internal ProjectInfo(
        string projectPath,
        string assemblyName,
        string rootNamespace,
        string relativeDirectory,
        IReadOnlyList<string> sourceFiles,
        Func<Compilation> compilationFactory,
        ProjectId projectId,
        ImmutableArray<DocumentId> documentIds,
        ImmutableDictionary<DocumentId, string> documentFilePaths,
        ImmutableDictionary<DocumentId, VersionStamp> documentVersions,
        VersionStamp projectVersion)
        : this(
            projectPath,
            assemblyName,
            rootNamespace,
            relativeDirectory,
            sourceFiles,
            compilationFactory,
            isRoslyn: true,
            projectId,
            documentIds,
            documentFilePaths,
            documentVersions,
            projectVersion)
    {
    }

    private ProjectInfo(
        string projectPath,
        string assemblyName,
        string rootNamespace,
        string relativeDirectory,
        IReadOnlyList<string> sourceFiles,
        Func<Compilation> compilationFactory,
        bool isRoslyn,
        ProjectId? projectId,
        ImmutableArray<DocumentId> documentIds,
        ImmutableDictionary<DocumentId, string> documentFilePaths,
        ImmutableDictionary<DocumentId, VersionStamp> documentVersions,
        VersionStamp? projectVersion)
    {
        ProjectPath = projectPath;
        AssemblyName = assemblyName;
        RootNamespace = rootNamespace;
        RelativeDirectory = relativeDirectory;
        SourceFiles = sourceFiles;
        _compilationFactory = new Lazy<Compilation>(compilationFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        _isRoslyn = isRoslyn;
        _projectId = projectId;
        _documentIds = documentIds;
        _documentFilePaths = documentFilePaths;
        DocumentVersions = documentVersions;
        RoslynProjectVersion = projectVersion;
    }

    public string ProjectPath { get; }
    public string AssemblyName { get; }
    public string RootNamespace { get; }
    public string RelativeDirectory { get; }
    public IReadOnlyList<string> SourceFiles { get; }

    public Compilation Compilation => _compilationFactory.Value;

    public bool IsRoslyn => _isRoslyn;
    public ProjectId? ProjectId => _projectId;
    public IReadOnlyList<DocumentId> DocumentIds => _documentIds;
    internal IReadOnlyDictionary<DocumentId, string> DocumentFilePaths => _documentFilePaths;

    public SemanticModel GetModel(SyntaxTree tree)
    {
        if (tree is null)
        {
            throw new ArgumentNullException(nameof(tree));
        }

        var compilation = Compilation;
        if (!compilation.SyntaxTrees.Contains(tree))
        {
            if (TryGetSyntaxTree(tree.FilePath, out var mapped))
            {
                tree = mapped;
            }
        }

        return _semanticModels.GetOrAdd(tree, static (syntaxTree, state) => state.Compilation.GetSemanticModel(syntaxTree), this);
    }

    public Task<SemanticModel> GetSemanticModelAsync(DocumentId documentId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_isRoslyn)
        {
            throw new InvalidOperationException("Semantic models are only available for Roslyn-backed projects.");
        }

        if (!_documentFilePaths.TryGetValue(documentId, out var path))
        {
            throw new ArgumentException($"Document '{documentId}' does not belong to project '{AssemblyName}'.", nameof(documentId));
        }

        if (!TryGetSyntaxTree(path, out var tree))
        {
            throw new InvalidOperationException($"Syntax tree for document '{path}' could not be resolved.");
        }

        return Task.FromResult(GetModel(tree));
    }

    public bool TryGetSyntaxTree(string? filePath, out SyntaxTree tree)
    {
        tree = null!;
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return false;
        }

        var cache = Volatile.Read(ref _syntaxTreeCache);
        if (cache is null)
        {
            cache = BuildSyntaxTreeCache();
        }

        return cache.TryGetValue(Path.GetFullPath(filePath), out tree);
    }

    private ImmutableDictionary<string, SyntaxTree> BuildSyntaxTreeCache()
    {
        lock (_syntaxTreeLock)
        {
            if (_syntaxTreeCache is { } existing)
            {
                return existing;
            }

            var builder = ImmutableDictionary.CreateBuilder<string, SyntaxTree>(StringComparer.OrdinalIgnoreCase);
            foreach (var tree in Compilation.SyntaxTrees)
            {
                if (string.IsNullOrWhiteSpace(tree.FilePath))
                {
                    continue;
                }

                try
                {
                    builder[Path.GetFullPath(tree.FilePath)] = tree;
                }
                catch (Exception)
                {
                    // Path might be malformed; skip caching.
                }
            }

            var cache = builder.ToImmutable();
            _syntaxTreeCache = cache;
            return cache;
        }
    }
}
