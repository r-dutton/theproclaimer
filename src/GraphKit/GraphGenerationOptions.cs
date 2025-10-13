using System.Collections.Generic;

namespace GraphKit;

public sealed record GraphGenerationOptions(
    string WorkspacePath,
    string OutputDirectory,
    IReadOnlyList<string>? Solutions = null);
