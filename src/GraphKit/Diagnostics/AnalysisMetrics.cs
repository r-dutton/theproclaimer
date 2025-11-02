using System.Diagnostics;

namespace GraphKit.Diagnostics
{
    public sealed class AnalysisMetrics
    {
        public int MethodsAnalyzed;
        public int InterprocExpansions;
        public Stopwatch Stopwatch = Stopwatch.StartNew();
    }
}
