using System;
using System.Collections.Concurrent;
using Microsoft.CodeAnalysis;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Interprocedural;

namespace GraphKit.Interproc
{
    public sealed class ExpansionManager
    {
        private readonly FlowInterproceduralConfig _cfg;
        private readonly ConcurrentDictionary<IMethodSymbol, bool> _visited =
            new(SymbolEqualityComparer.Default);

        public ExpansionManager(FlowInterproceduralConfig cfg) => _cfg = cfg;

        public void Analyze(Compilation compilation, IMethodSymbol target,
            SemanticModel model, FlowDataFlowOperationVisitor visitor)
        {
            if (target is null) return;
            if (_visited.ContainsKey(target)) return;
            _visited[target] = true;

            GraphKit.FlowAnalysis.Core.FlowAnalysis.AnalyzeMethod(compilation, model, target, _cfg,
                inv => true /* predicate controlled in visitors */,
                visitor);
        }
    }
}
