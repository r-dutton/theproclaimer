using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphKit.Outputs.Abstractions;

namespace GraphKit.Outputs.FlowBuilder
{
    public static class FlowBuilderJson
    {
        public static void Render(IGraphProvider provider, string outDir)
        {
            Directory.CreateDirectory(outDir);
            var graph = FlowBuilderCore.BuildGraph(provider);
            var nodes = graph.Nodes.Select(n => new
            {
                n.Id,
                n.Type,
                Props = n.Props
            }).ToArray();
            var edges = graph.Edges.Select(e => new
            {
                e.FromId,
                e.ToId,
                e.Kind,
                Props = e.Props
            }).ToArray();
            var dto = new { nodes, edges };
            var opts = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            File.WriteAllText(Path.Combine(outDir, "graph.json"), JsonSerializer.Serialize(dto, opts));
        }
    }
}
