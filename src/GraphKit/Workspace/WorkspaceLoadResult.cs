using System;
using System.Collections.Generic;

namespace GraphKit.Workspace;

public sealed record WorkspaceLoadResult(
    IReadOnlyList<ProjectInfo> Projects,
    IReadOnlyList<RoslynProjectInfo> RoslynProjects)
{
    public static readonly WorkspaceLoadResult Empty = new(
        Array.Empty<ProjectInfo>(),
        Array.Empty<RoslynProjectInfo>());
}
