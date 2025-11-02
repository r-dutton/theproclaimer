using System;
using System.Collections.Generic;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class ControllerOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly ControllerActionInfo _action;
        private readonly HashSet<string> _seenRequests = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenNotifications = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenMappings = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenHttpCalls = new(StringComparer.OrdinalIgnoreCase);

        public ControllerOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            ControllerActionInfo action,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _action = action;
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            if (AnalysisPredicates.IsMediatorSend(op))
            {
                HandleMediatorSend(op);
            }
            else if (AnalysisPredicates.IsMediatorPublish(op))
            {
                HandleMediatorPublish(op);
            }
            else if (AnalysisPredicates.IsMapperMap(op))
            {
                HandleMapperMap(op);
            }
            else if (AnalysisPredicates.IsHttpClientCall(op))
            {
                HandleHttpClientCall(op);
            }

            base.VisitInvocation(op);
        }

        private void HandleMediatorSend(IInvocationOperation invocation)
        {
            var requestArgument = invocation.Arguments.Length > 0 ? invocation.Arguments[0].Value : null;
            var requestType = Qualify(requestArgument?.Type);
            if (string.IsNullOrWhiteSpace(requestType))
            {
                return;
            }

            var line = GetInvocationLine(invocation);
            var key = $"{requestType}@{line}";
            if (!_seenRequests.Add(key))
            {
                return;
            }

            _action.RequestInvocations.Add(new ControllerRequestInvocation(requestType, line));
        }

        private void HandleMediatorPublish(IInvocationOperation invocation)
        {
            var messageArgument = invocation.Arguments.Length > 0 ? invocation.Arguments[0].Value : null;
            var notificationType = Qualify(messageArgument?.Type);
            if (string.IsNullOrWhiteSpace(notificationType))
            {
                return;
            }

            var line = GetInvocationLine(invocation);
            var key = $"{notificationType}@{line}";
            if (!_seenNotifications.Add(key))
            {
                return;
            }

            _action.NotificationInvocations.Add(new ControllerNotificationInvocation(notificationType, line));
        }

        private void HandleMapperMap(IInvocationOperation invocation)
        {
            var destinationType = Qualify(GetDestinationType(invocation));
            if (string.IsNullOrWhiteSpace(destinationType))
            {
                return;
            }

            var sourceArgument = invocation.Arguments.Length > 0 ? invocation.Arguments[0].Value : null;
            var sourceType = Qualify(sourceArgument?.Type);

            var line = GetInvocationLine(invocation);
            var key = $"{destinationType}@{sourceType}@{line}";
            if (!_seenMappings.Add(key))
            {
                return;
            }

            _action.MappingInvocations.Add(new ControllerMappingInvocation(sourceType, destinationType, null, line));
        }

        private void HandleHttpClientCall(IInvocationOperation invocation)
        {
            var clientSymbol = invocation.Instance?.Type ?? invocation.TargetMethod.ContainingType;
            var clientType = Qualify(clientSymbol) ??
                             clientSymbol?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat) ??
                             "System.Net.Http.HttpClient";

            if (string.IsNullOrWhiteSpace(clientType))
            {
                return;
            }

            var methodName = invocation.TargetMethod.Name;
            var verb = NormalizeHttpVerb(methodName);

            var route = TryResolveRoute(invocation);
            if (!string.IsNullOrWhiteSpace(route))
            {
                route = NormalizeRoute(route!);
            }

            var line = GetInvocationLine(invocation);
            var key = $"{clientType}@{methodName}@{route}@{line}";
            if (!_seenHttpCalls.Add(key))
            {
                return;
            }

            _action.HttpClientInvocations.Add(new ControllerClientInvocation(
                clientType,
                verb,
                route,
                line,
                methodName));
        }

        private string? Qualify(ITypeSymbol? symbol)
        {
            if (symbol is null)
            {
                return null;
            }

            var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            var qualified = _analyzer.QualifyTypeName(display, _action.Assembly, _action.Project);
            return string.IsNullOrWhiteSpace(qualified) ? display : qualified;
        }

        private static ITypeSymbol? GetDestinationType(IInvocationOperation invocation)
        {
            if (invocation.TargetMethod.IsGenericMethod && invocation.TargetMethod.TypeArguments.Length > 0)
            {
                return invocation.TargetMethod.TypeArguments[0];
            }

            return invocation.TargetMethod.ReturnType;
        }

        private static int GetInvocationLine(IInvocationOperation invocation)
        {
            if (invocation.Syntax?.SyntaxTree is { } tree)
            {
                return GetLineNumber(tree, invocation.Syntax);
            }

            return 0;
        }

        private string? TryResolveRoute(IInvocationOperation invocation)
        {
            foreach (var argument in invocation.Arguments)
            {
                if (!IsRouteParameter(argument.Parameter))
                {
                    continue;
                }

                var literal = TryGetStringLiteral(argument.Value) ?? ValueContent.TryGetStringValue(argument.Value);
                if (!string.IsNullOrWhiteSpace(literal))
                {
                    return literal;
                }
            }

            if (invocation.Arguments.Length > 0)
            {
                var literal = TryGetStringLiteral(invocation.Arguments[0].Value) ??
                              ValueContent.TryGetStringValue(invocation.Arguments[0].Value);
                if (!string.IsNullOrWhiteSpace(literal))
                {
                    return literal;
                }
            }

            return null;
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

        private static string? TryGetStringLiteral(IOperation? operation)
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
                return TryGetStringLiteral(conversion.Operand);
            }

            return null;
        }
    }
}
