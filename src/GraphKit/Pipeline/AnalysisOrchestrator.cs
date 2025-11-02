using GraphKit.Facts;
using GraphKit.Outputs.Facts;

namespace GraphKit.Pipeline
{
    public static class AnalysisOrchestrator
    {
        public static void RunAndExport(FactWriter writer, string outputPath)
        {
            var bag = GraphKit.Outputs.Facts.FactsPipeline.Finalize(writer);
            FactsJsonWriter.Write(bag, outputPath);
        }
    }
}
