using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace GraphKit.Workspace;

public sealed class ProjectInfo
{
    private readonly Lazy<Compilation> _compilationFactory;
    private readonly ConcurrentDictionary<SyntaxTree, SemanticModel> _semanticModels = new();

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

        return _semanticModels.GetOrAdd(tree, static (syntaxTree, state) => state.Compilation.GetSemanticModel(syntaxTree), this);
    }
}
