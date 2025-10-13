namespace GraphKit.Workspace;

public sealed record ProjectInfo(
    string ProjectPath,
    string AssemblyName,
    string RootNamespace,
    string RelativeDirectory,
    IReadOnlyList<string> SourceFiles);
