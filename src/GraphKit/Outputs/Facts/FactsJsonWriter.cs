using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphKit.Facts;

namespace GraphKit.Outputs.Facts
{
    public static class FactsJsonWriter
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static void Write(FactBag bag, string path)
        {
            var dto = new { nodes = bag.Nodes, edges = bag.Edges };
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!);
            File.WriteAllText(path, JsonSerializer.Serialize(dto, Options));
        }
    }
}
