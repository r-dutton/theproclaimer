using System.Collections.Generic;

namespace GraphKit.Facts
{
    public sealed class EdgeFact : IFlowFact
    {
        public string FromId { get; }
        public string ToId { get; }
        public string Kind { get; }
        public Dictionary<string, object?> Props { get; }

        public EdgeFact(string fromId, string toId, string kind, Dictionary<string, object?>? props = null)
        {
            FromId = fromId;
            ToId = toId;
            Kind = kind;
            Props = props ?? new Dictionary<string, object?>();
        }
    }
}
