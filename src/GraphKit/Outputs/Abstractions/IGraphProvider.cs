using System.Collections.Generic;

namespace GraphKit.Outputs.Abstractions
{
    public interface IGraphProvider
    {
        IEnumerable<(string Id, string Type, IReadOnlyDictionary<string, object?> Props)> Nodes();
        IEnumerable<(string FromId, string ToId, string Kind, IReadOnlyDictionary<string, object?> Props)> Edges();
    }
}
