using System;
using System.Collections.Generic;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class MessagingOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly string _assembly;
        private readonly string _project;
        private readonly string _ownerMethod;
        private readonly Action<string, string, string?, int, string> _recordPublish;
        private readonly HashSet<string> _seenPublishes = new(StringComparer.OrdinalIgnoreCase);

        public MessagingOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            string assembly,
            string project,
            string ownerMethod,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            Action<string, string, string?, int, string> recordPublish)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _assembly = assembly;
            _project = project;
            _ownerMethod = ownerMethod;
            _recordPublish = recordPublish;
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            if (IsBusPublish(op))
            {
                HandleBusPublish(op);
            }

            base.VisitInvocation(op);
        }

        private bool IsBusPublish(IInvocationOperation invocation)
        {
            if (invocation.TargetMethod is not { } method)
            {
                return false;
            }

            var methodName = method.Name;
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return false;
            }

            if (!(methodName.StartsWith("Publish", StringComparison.OrdinalIgnoreCase) ||
                  methodName.StartsWith("Send", StringComparison.OrdinalIgnoreCase) ||
                  methodName.StartsWith("Enqueue", StringComparison.OrdinalIgnoreCase) ||
                  methodName.StartsWith("Dispatch", StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var receiver = invocation.Instance?.Type;
            if (receiver is null && method.IsExtensionMethod && invocation.Arguments.Length > 0)
            {
                receiver = invocation.Arguments[0].Value.Type;
            }

            receiver ??= method.ContainingType;
            if (receiver is null)
            {
                return false;
            }

            var display = receiver.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (IsLikelyPublisherTypeName(display))
            {
                return true;
            }

            if (display.Contains("Bus", StringComparison.OrdinalIgnoreCase) ||
                display.Contains("Queue", StringComparison.OrdinalIgnoreCase) ||
                display.Contains("Topic", StringComparison.OrdinalIgnoreCase) ||
                display.Contains("Dispatcher", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var simple = receiver.Name;
            return IsLikelyPublisherTypeName(simple) ||
                   simple.Contains("Bus", StringComparison.OrdinalIgnoreCase) ||
                   simple.Contains("Queue", StringComparison.OrdinalIgnoreCase) ||
                   simple.Contains("Topic", StringComparison.OrdinalIgnoreCase);
        }

        private void HandleBusPublish(IInvocationOperation invocation)
        {
            var publisherType = Qualify(invocation.Instance?.Type ?? invocation.TargetMethod.ContainingType);
            if (string.IsNullOrWhiteSpace(publisherType))
            {
                return;
            }

            var methodName = invocation.TargetMethod?.Name ?? string.Empty;
            var line = GetInvocationLine(invocation);
            var messageType = DetermineMessageType(invocation);

            var key = $"{publisherType}@{methodName}@{messageType}@{line}";
            if (!_seenPublishes.Add(key))
            {
                return;
            }

            _recordPublish(publisherType!, methodName, messageType, line, _ownerMethod);
        }

        private string? DetermineMessageType(IInvocationOperation invocation)
        {
            if (invocation.TargetMethod.IsGenericMethod &&
                invocation.TargetMethod.TypeArguments.Length > 0)
            {
                var genericType = invocation.TargetMethod.TypeArguments[0];
                return Qualify(genericType);
            }

            if (invocation.Arguments.Length > 0)
            {
                var argumentType = invocation.Arguments[0].Value.Type;
                if (argumentType is not null)
                {
                    return Qualify(argumentType);
                }
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
            var qualified = _analyzer.QualifyTypeName(display, _assembly, _project);
            return string.IsNullOrWhiteSpace(qualified) ? display : qualified;
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
