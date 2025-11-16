using System;
using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Http;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class HttpOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly HttpClientInfo _client;
        private readonly string _ownerMethod;
        private readonly HashSet<string> _seenCalls = new(StringComparer.OrdinalIgnoreCase);
        private readonly FactWriter _facts;

        public HttpOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            HttpClientInfo client,
            string ownerMethod,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            FlowNullAnalysisFacade nullAnalysis,
            FlowCopyAnalysisFacade copyAnalysis,
            FlowPredicateAnalysisFacade predicateAnalysis,
            FlowTaintedDataFacade taintedData,
            FactWriter facts)
            : base(model.Compilation, model, pointsTo, valueContent, nullAnalysis, copyAnalysis, predicateAnalysis, taintedData)
        {
            _analyzer = analyzer;
            _client = client;
            _ownerMethod = ownerMethod;
            _facts = facts ?? throw new ArgumentNullException(nameof(facts));
            _ = _facts;
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            if (AnalysisPredicates.IsHttpClientCall(op))
            {
                HandleHttpInvocation(op);
            }

            base.VisitInvocation(op);
        }

        private void HandleHttpInvocation(IInvocationOperation invocation)
        {
            if (PredicateAnalysis?.IsAlwaysFalse(invocation) ?? false)
            {
                return;
            }

            if (invocation.Instance is not null && (NullAnalysis?.IsDefinitelyNull(invocation.Instance) ?? false))
            {
                return;
            }

            if (!RouteCanonicalizer.TryReconstruct(invocation, ValueContent, out var verb, out var rawRoute))
            {
                return;
            }

            var normalizedRoute = RouteCanonicalizer.CanonRoute(rawRoute);
            var parameters = ExtractQueryParameters(rawRoute);
            var line = GetInvocationLine(invocation);
            var key = $"{_ownerMethod}@{verb}@{normalizedRoute}@{line}";
            if (!_seenCalls.Add(key))
            {
                return;
            }

            var containsTaint = TaintedData?.IsInvocationTainted(invocation) ?? false;

            var call = new HttpClientCall(
                _ownerMethod,
                verb,
                normalizedRoute,
                line,
                parameters,
                containsTaint);
            _client.OutboundCalls.Add(call);
            _analyzer.RecordHttpClientOutboundCallFact(_client, call);
        }

        private static int GetInvocationLine(IInvocationOperation invocation)
        {
            if (invocation.Syntax?.SyntaxTree is { } tree)
            {
                return GetLineNumber(tree, invocation.Syntax);
            }

            return 0;
        }

        private static IReadOnlyCollection<string> ExtractQueryParameters(string? route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                return Array.Empty<string>();
            }

            var queryIndex = route.IndexOf('?', StringComparison.Ordinal);
            if (queryIndex < 0 || queryIndex == route.Length - 1)
            {
                return Array.Empty<string>();
            }

            var query = route[(queryIndex + 1)..];
            if (string.IsNullOrWhiteSpace(query))
            {
                return Array.Empty<string>();
            }

            var segments = query.Split('&', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (segments.Length == 0)
            {
                return Array.Empty<string>();
            }

            var parameters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var segment in segments)
            {
                var equalsIndex = segment.IndexOf('=');
                var name = equalsIndex >= 0 ? segment[..equalsIndex] : segment;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    parameters.Add(name);
                }
            }

            return parameters.Count == 0 ? Array.Empty<string>() : parameters.ToArray();
        }
    }
}
