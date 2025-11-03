using System;
using Microsoft.CodeAnalysis;

namespace GraphKit.Workspace;

public sealed class RoslynProjectInfo
{
    public RoslynProjectInfo(ProjectInfo project, Project roslynProject)
    {
        Project = project ?? throw new ArgumentNullException(nameof(project));
        RoslynProject = roslynProject ?? throw new ArgumentNullException(nameof(roslynProject));
    }

    public ProjectInfo Project { get; }

    public Project RoslynProject { get; }
}
