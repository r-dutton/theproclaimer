using GraphKit.Facts;
using GraphKit.Graph;

namespace GraphKit
{
    public sealed class GraphGenerationResult
    {
        public GraphGenerationResult(GraphDocument document, FactBag facts)
        {
            Document = document;
            Facts = facts;
        }

        public GraphDocument Document { get; }
        public FactBag Facts { get; }
    }
}
