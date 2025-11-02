using System.Collections.Generic;

namespace GraphKit.Facts
{
    public sealed class NodeFact : IFlowFact
    {
        public string Id { get; }
        public string Type { get; }
        public Dictionary<string, object?> Props { get; }

        public NodeFact(string id, string type, Dictionary<string, object?>? props = null)
        {
            Id = id;
            Type = type;
            Props = props ?? new Dictionary<string, object?>();
        }
    }
}
