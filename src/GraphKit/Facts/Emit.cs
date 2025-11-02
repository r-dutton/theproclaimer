using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.Constants;
using GraphKit.Model;

namespace GraphKit.Facts
{
    public static class Emit
    {
        public static void Node(FactWriter w, string id, string type, IDictionary<string, object?>? props = null)
        {
            var p = props is null ? new Dictionary<string, object?>() : new Dictionary<string, object?>(props);
            w.AddNode(new NodeFact(id, type, p));
        }

        public static void Edge(FactWriter w, string from, string to, string kind,
            Provenance prov, Confidence conf, IDictionary<string, object?>? props = null)
        {
            var p = props is null ? new Dictionary<string, object?>() : new Dictionary<string, object?>(props);
            p[PropKeys.Provenance] = prov.ToString();
            p[PropKeys.Confidence] = conf.ToString();
            w.AddEdge(new EdgeFact(from, to, kind, p));
        }
    }
}
