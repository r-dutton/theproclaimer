using System;
using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class CqrsOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly HandlerInfo _handler;
        private readonly string _ownerMethod;
        private readonly EfOperationVisitor _efVisitor;
        private readonly HashSet<string> _seenDbAccesses = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenRepositoryCalls = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenMapperCalls = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenHttpCalls = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenNotifications = new(StringComparer.OrdinalIgnoreCase);
        private readonly FactWriter _facts;

        public CqrsOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            HandlerInfo handler,
            string ownerMethod,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            FactWriter facts)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _handler = handler;
            _ownerMethod = ownerMethod;
            _facts = facts ?? throw new ArgumentNullException(nameof(facts));
            _ = _facts;
            _efVisitor = new EfOperationVisitor(analyzer, handler.Assembly, handler.Project, RecordEfAccess, _facts);
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            _efVisitor.TryProcess(op);

            if (AnalysisPredicates.IsDbContextOrRepoCall(op))
            {
                HandleDataCall(op);
            }
            else if (AnalysisPredicates.IsMediatorPublish(op) || AnalysisPredicates.IsDomainEventPublish(op))
            {
                HandleMediatorPublish(op);
            }
            else if (AnalysisPredicates.IsMapperMap(op))
            {
                HandleMapperCall(op);
            }
            else if (AnalysisPredicates.IsHttpClientCall(op))
            {
                HandleHttpCall(op);
            }

            base.VisitInvocation(op);
        }

        private void HandleDataCall(IInvocationOperation invocation)
        {
            var receiverSymbol = invocation.Instance?.Type ?? invocation.TargetMethod.ContainingType;
            var typeName = Qualify(receiverSymbol);
            var methodName = invocation.TargetMethod.Name ?? string.Empty;
            var line = GetInvocationLine(invocation);

            if (!string.IsNullOrWhiteSpace(typeName) && IsRepositoryType(typeName))
            {
                var operation = DetermineRepositoryOperation(methodName);
                var key = $"{typeName}@{methodName}@{line}";
                if (_seenRepositoryCalls.Add(key))
                {
                    _handler.RepositoryCalls.Add(new HandlerRepositoryCall(typeName, methodName, line, operation));
                    _analyzer.RecordHandlerRepositoryFact(_handler, typeName!, methodName, operation, line);
                }
                return;
            }
        }

        private void HandleMediatorPublish(IInvocationOperation invocation)
        {
            var argument = invocation.Arguments.Length > 0 ? invocation.Arguments[0].Value : null;
            var notificationType = Qualify(argument?.Type);
            if (string.IsNullOrWhiteSpace(notificationType))
            {
                return;
            }

            var line = GetInvocationLine(invocation);
            var key = $"{notificationType}@{line}";
            if (_seenNotifications.Add(key))
            {
                _handler.PublishedNotifications.Add(new HandlerNotificationPublication(notificationType, line));
                _analyzer.RecordHandlerNotificationFact(_handler, notificationType, line);
            }
        }

        private void HandleMapperCall(IInvocationOperation invocation)
        {
            var destinationType = Qualify(GetDestinationType(invocation));
            if (string.IsNullOrWhiteSpace(destinationType))
            {
                return;
            }

            var sourceOperation = invocation.Arguments.Length > 0 ? invocation.Arguments[0].Value : null;
            var sourceType = Qualify(sourceOperation?.Type);

            var line = GetInvocationLine(invocation);
            var key = $"{destinationType}@{sourceType}@{line}";
            if (_seenMapperCalls.Add(key))
            {
                _handler.MapperCalls.Add(new HandlerMapperCall(sourceType, destinationType, line));
                _analyzer.RecordHandlerMappingFact(_handler, sourceType, destinationType!, line);
            }
        }

        private void HandleHttpCall(IInvocationOperation invocation)
        {
            var clientSymbol = invocation.Instance?.Type ?? invocation.TargetMethod.ContainingType;
            var clientType = Qualify(clientSymbol) ??
                             clientSymbol?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat) ??
                             "System.Net.Http.HttpClient";

            if (string.IsNullOrWhiteSpace(clientType))
            {
                return;
            }

            var verb = NormalizeHttpVerb(invocation.TargetMethod.Name);
            var route = TryResolveRoute(invocation);
            if (!string.IsNullOrWhiteSpace(route))
            {
                route = NormalizeRoute(route!);
            }

            var line = GetInvocationLine(invocation);
            var key = $"{clientType}@{verb}@{route}@{line}";
            if (!_seenHttpCalls.Add(key))
            {
                return;
            }

            _handler.HttpClientInvocations.Add(new HandlerClientInvocation(
                clientType,
                verb ?? string.Empty,
                route,
                line,
                invocation.TargetMethod.Name,
                null,
                null,
                _ownerMethod));
            _analyzer.RecordHandlerHttpClientFact(_handler, clientType, verb, route, invocation.TargetMethod.Name, line, _ownerMethod);
        }

        private void RecordEfAccess(string? contextType, string entityName, string operation, int line)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return;
            }

            var key = $"{entityName}@{line}";
            if (!_seenDbAccesses.Add(key))
            {
                return;
            }

            var context = string.IsNullOrWhiteSpace(contextType) ? entityName : contextType!;
            _handler.DbContextAccesses.Add(new HandlerDbAccess(context, entityName, line));
            _analyzer.RecordHandlerEfAccessFact(_handler, context, entityName, operation, line);
        }

        private string? Qualify(ITypeSymbol? symbol)
        {
            if (symbol is null)
            {
                return null;
            }

            var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            var qualified = _analyzer.QualifyTypeName(display, _handler.Assembly, _handler.Project);
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
