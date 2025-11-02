using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IO;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace GraphKit.Workspace;

public sealed class ProjectInfo
{
    private readonly Lazy<Compilation> _compilationFactory;
    private readonly ConcurrentDictionary<SyntaxTree, SemanticModel> _semanticModels = new();
    private ImmutableDictionary<string, SyntaxTree>? _syntaxTreeCache;
    private readonly object _syntaxTreeLock = new();

    public ProjectInfo(
        string projectPath,
        string assemblyName,
        string rootNamespace,
        string relativeDirectory,
        IReadOnlyList<string> sourceFiles,
        Func<Compilation> compilationFactory)
    {
        ProjectPath = projectPath;
        AssemblyName = assemblyName;
        RootNamespace = rootNamespace;
        RelativeDirectory = relativeDirectory;
        SourceFiles = sourceFiles;
        _compilationFactory = new Lazy<Compilation>(compilationFactory, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public string ProjectPath { get; }
    public string AssemblyName { get; }
    public string RootNamespace { get; }
    public string RelativeDirectory { get; }
    public IReadOnlyList<string> SourceFiles { get; }

    public Compilation Compilation => _compilationFactory.Value;

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
