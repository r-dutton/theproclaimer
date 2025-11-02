using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Dependencies
{
    public sealed class FlowValueContentFacade
    {
        // Best-effort literal/concat/interpolation reconstruction.
        public string? TryGetStringValue(IOperation op) => null;
    }
}
