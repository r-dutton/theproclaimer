using System;
using System.Collections.Concurrent;
using Microsoft.CodeAnalysis;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using FlowAnalysisCore = GraphKit.FlowAnalysis.Core.FlowAnalysis;

namespace GraphKit.Interproc
{
    public sealed class ExpansionManager
    {
        private readonly InterproceduralAnalysisConfiguration _configuration;
        private readonly ConcurrentDictionary<IMethodSymbol, bool> _visited =
            new(SymbolEqualityComparer.Default);

        public ExpansionManager(InterproceduralAnalysisConfiguration configuration)
            => _configuration = configuration;

        public void Analyze(Compilation compilation, IMethodSymbol target,
            SemanticModel model, FlowDataFlowOperationVisitor visitor)
        {
            if (target is null) return;
            if (_visited.ContainsKey(target)) return;
            _visited[target] = true;

            var settings = new InterproceduralSettings(
                _configuration.InterproceduralAnalysisKind,
                (int)_configuration.MaxInterproceduralMethodCallChain,
                (int)_configuration.MaxInterproceduralLambdaOrLocalFunctionCallChain);

            var analysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(compilation, target, settings);
            analysis.Context.Accept(visitor);
        }
    }
}
