using System;
using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class DomainEventsOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly DomainEventHandlerInfo _handler;
        private readonly string _ownerMethod;
        private readonly HashSet<string> _seenRepositoryCalls = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenRequests = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenNotifications = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _seenMappings = new(StringComparer.OrdinalIgnoreCase);
        private readonly FactWriter _facts;

        public DomainEventsOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            DomainEventHandlerInfo handler,
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
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            if (AnalysisPredicates.IsMediatorSend(op))
            {
                HandleMediatorSend(op);
            }
            else if (AnalysisPredicates.IsMediatorPublish(op) || AnalysisPredicates.IsDomainEventPublish(op))
            {
                HandleMediatorPublish(op);
            }
            else if (AnalysisPredicates.IsMapperMap(op))
            {
                HandleMapperMap(op);
            }
            else if (AnalysisPredicates.IsDbContextOrRepoCall(op))
            {
                HandleRepositoryCall(op);
            }

            base.VisitInvocation(op);
        }

        private void HandleMediatorSend(IInvocationOperation invocation)
        {
            var requestType = DetermineRequestType(invocation);
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

            _handler.RequestInvocations.Add(new NotificationHandlerRequestInvocation(requestType!, line));
            _analyzer.RecordDomainEventHandlerRequestFact(_handler, requestType!, line);
        }

        private void HandleMediatorPublish(IInvocationOperation invocation)
        {
            var notificationType = DetermineNotificationType(invocation);
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

            _handler.PublishedNotifications.Add(new HandlerNotificationPublication(notificationType!, line));
            _analyzer.RecordDomainEventHandlerPublishFact(_handler, notificationType!, line);
        }

        private void HandleMapperMap(IInvocationOperation invocation)
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
            if (!_seenMappings.Add(key))
            {
                return;
            }

            _handler.MapperCalls.Add(new HandlerMapperCall(sourceType, destinationType, line));
            _analyzer.RecordDomainEventHandlerMappingFact(_handler, sourceType, destinationType!, line);
        }

        private void HandleRepositoryCall(IInvocationOperation invocation)
        {
            var receiverSymbol = invocation.Instance?.Type ?? invocation.TargetMethod.ContainingType;
            var typeName = Qualify(receiverSymbol);
            if (string.IsNullOrWhiteSpace(typeName) || !IsRepositoryType(typeName))
            {
                return;
            }

            var methodName = invocation.TargetMethod.Name ?? string.Empty;
            var line = GetInvocationLine(invocation);
            var key = $"{typeName}@{methodName}@{line}";
            if (!_seenRepositoryCalls.Add(key))
            {
                return;
            }

            var operation = DetermineRepositoryOperation(methodName);
            var entityType = ExtractRepositoryEntityType(typeName!);
            if (entityType is null &&
                invocation.Syntax is InvocationExpressionSyntax invocationSyntax &&
                invocationSyntax.Expression is MemberAccessExpressionSyntax access)
            {
                entityType = ExtractRepositoryEntityTypeFromInvocation(access.Name, invocationSyntax);
            }

            _handler.RepositoryCalls.Add(new NotificationHandlerRepositoryCall(typeName!, entityType, methodName, line, operation));
            _analyzer.RecordDomainEventHandlerRepositoryFact(_handler, typeName!, methodName, operation, line);
        }

        private string? DetermineRequestType(IInvocationOperation invocation)
        {
            if (invocation.Arguments.Length > 0)
            {
                var argType = invocation.Arguments[0].Value.Type;
                if (argType is not null)
                {
                    return Qualify(argType);
                }
            }

            if (invocation.TargetMethod.IsGenericMethod && invocation.TargetMethod.TypeArguments.Length > 0)
            {
                return Qualify(invocation.TargetMethod.TypeArguments[0]);
            }

            return null;
        }

        private string? DetermineNotificationType(IInvocationOperation invocation)
        {
            if (invocation.Arguments.Length > 0)
            {
                var argType = invocation.Arguments[0].Value.Type;
                if (argType is not null)
                {
                    return Qualify(argType);
                }
            }

            if (invocation.TargetMethod.IsGenericMethod && invocation.TargetMethod.TypeArguments.Length > 0)
            {
                return Qualify(invocation.TargetMethod.TypeArguments[0]);
            }

            return null;
        }

        private string? Qualify(ITypeSymbol? symbol)
        {
            if (symbol is null)
            {
                return null;
            }

            var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            var qualified = _analyzer.QualifyTypeName(display, _handler.Assembly ?? string.Empty, _handler.Project ?? string.Empty);
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
    }
}
