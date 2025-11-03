using System.Collections.Generic;
using GraphKit.Analyzers;
using Microsoft.CodeAnalysis.MSBuild;

namespace GraphKit;

public sealed record GraphGenerationOptions(
    string WorkspacePath,
    string OutputDirectory,
    IReadOnlyList<string>? Solutions = null,
    bool UseRoslyn = false,
    MSBuildWorkspace? RoslynWorkspace = null,
    ProjectAnalyzer.ProjectAnalyzerConfiguration? AnalyzerConfiguration = null);
