using System;
using System.Collections.Generic;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
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

        public HttpOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            HttpClientInfo client,
            string ownerMethod,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _client = client;
            _ownerMethod = ownerMethod;
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
            var verb = NormalizeHttpVerb(invocation.TargetMethod.Name) ?? invocation.TargetMethod.Name.ToUpperInvariant();
            var route = TryResolveRoute(invocation);
            var normalizedRoute = route is null ? null : NormalizeRoute(route);
            var parameters = ExtractQueryParameters(route);
            var line = GetInvocationLine(invocation);
            var key = $"{_ownerMethod}@{verb}@{normalizedRoute}@{line}";
            if (!_seenCalls.Add(key))
            {
                return;
            }

            _client.OutboundCalls.Add(new HttpClientCall(
                _ownerMethod,
                verb,
                normalizedRoute,
                line,
                parameters));
        }

        private string? TryResolveRoute(IInvocationOperation invocation)
        {
            foreach (var argument in invocation.Arguments)
            {
                if (!IsRouteParameter(argument.Parameter))
                {
                    continue;
                }

                var value = TryGetString(argument.Value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            if (invocation.Arguments.Length > 0)
            {
                var value = TryGetString(invocation.Arguments[0].Value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }

        private string? TryGetString(IOperation? operation)
        {
            if (operation is null)
            {
                return null;
            }

            if (operation.ConstantValue is { HasValue: true, Value: string s })
            {
                return s;
            }

            if (operation is IConversionOperation conversion)
            {
                return TryGetString(conversion.Operand);
            }

            return ValueContent.TryGetStringValue(operation);
        }

        private static bool IsRouteParameter(IParameterSymbol? parameter)
        {
            if (parameter is null)
            {
                return false;
            }

            return parameter.Name.Equals("requestUri", StringComparison.OrdinalIgnoreCase) ||
                   parameter.Name.Equals("uri", StringComparison.OrdinalIgnoreCase) ||
                   parameter.Name.Equals("url", StringComparison.OrdinalIgnoreCase) ||
                   parameter.Name.Equals("endpoint", StringComparison.OrdinalIgnoreCase) ||
                   parameter.Name.Equals("path", StringComparison.OrdinalIgnoreCase);
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
