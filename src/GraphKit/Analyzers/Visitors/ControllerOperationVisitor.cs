using System;
using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            private readonly FactWriter _facts;
            private readonly ProjectInfo _project;
            private readonly IReadOnlyDictionary<string, string?> _parameterTypes;
            private readonly IReadOnlyDictionary<string, FieldDescriptor> _fieldLookup;

        public ControllerOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            ControllerActionInfo action,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            FactWriter facts,
            ProjectInfo project,
            IReadOnlyDictionary<string, string?> parameterTypes,
            IReadOnlyDictionary<string, FieldDescriptor> fieldLookup)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _action = action;
            _facts = facts ?? throw new ArgumentNullException(nameof(facts));
            _project = project ?? throw new ArgumentNullException(nameof(project));
            _parameterTypes = parameterTypes ?? throw new ArgumentNullException(nameof(parameterTypes));
            _fieldLookup = fieldLookup ?? throw new ArgumentNullException(nameof(fieldLookup));
            _ = _facts;
        }

        private static readonly IReadOnlyDictionary<string, int> StatusHelperCodes = new Dictionary<string, int>(StringComparer.Ordinal)
        {
            ["Ok"] = 200,
            ["Created"] = 201,
            ["CreatedAtAction"] = 201,
            ["CreatedAtRoute"] = 201,
            ["NoContent"] = 204,
            ["BadRequest"] = 400,
            ["Unauthorized"] = 401,
            ["Forbidden"] = 403,
            ["NotFound"] = 404,
            ["Conflict"] = 409,
            ["Problem"] = 500
        };

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

            HandleStatusCodeInvocation(op);
            TryHandleServiceInvocation(op);
            TryHandleHelperInvocation(op);

            base.VisitInvocation(op);
        }

        private void TryHandleHelperInvocation(IInvocationOperation invocation)
        {
            var method = invocation.TargetMethod;
            if (method is null)
            {
                return;
            }

            var containing = method.ContainingType;
            if (containing is null)
            {
                return;
            }

            var controllerFqdn = containing.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            var controllerSymbolId = _action.ControllerSymbolId;
            if (string.IsNullOrWhiteSpace(controllerFqdn) || string.IsNullOrWhiteSpace(controllerSymbolId))
            {
                return;
            }

            var containingSymbolId = $"T:{controllerFqdn}";
            // match only helper methods on the same controller type and that are not public
            if (!string.Equals(containingSymbolId, controllerSymbolId, StringComparison.Ordinal) ||
                method.DeclaredAccessibility == Accessibility.Public)
            {
                return;
            }

            var helperName = method.Name;
            var helperKey = BuildMethodSymbolId(controllerFqdn, method);
            var line = GetInvocationLine(invocation);
            _action.HelperInvocations.Add(new ControllerHelperInvocation(helperKey, $"{controllerFqdn}.{helperName}", line));
        }

        private static string BuildMethodSymbolId(string controllerFqdn, IMethodSymbol method)
        {
            if (string.IsNullOrWhiteSpace(controllerFqdn) || string.IsNullOrWhiteSpace(method.Name))
            {
                return $"M:{controllerFqdn}.{method.Name}";
            }

            if (method.Parameters.Length == 0)
            {
                return $"M:{controllerFqdn}.{method.Name}";
            }

            static string? MapRefKind(RefKind kind)
                => kind switch { RefKind.Ref => "ref", RefKind.Out => "out", RefKind.In => "in", _ => null };

            var parts = new List<string>(method.Parameters.Length);
            foreach (var p in method.Parameters)
            {
                var typeName = p.Type?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (string.IsNullOrWhiteSpace(typeName))
                {
                    typeName = "object";
                }

                var modifier = MapRefKind(p.RefKind);
                var segment = string.IsNullOrWhiteSpace(modifier) ? typeName : $"{modifier} {typeName}";
                parts.Add(segment!.Trim());
            }

            var signature = string.Join(",", parts);
            return $"M:{controllerFqdn}.{method.Name}({signature})";
        }

        private void HandleStatusCodeInvocation(IInvocationOperation invocation)
        {
            if (invocation.TargetMethod is null)
            {
                return;
            }

            if (!StatusHelperCodes.TryGetValue(invocation.TargetMethod.Name, out var status))
            {
                return;
            }

            if (!IsReturnContext(invocation))
            {
                return;
            }

            _action.StatusCodes.Add(status);
        }

        private static bool IsReturnContext(IInvocationOperation invocation)
        {
            var parent = invocation.Parent;
            if (parent is IReturnOperation)
            {
                return true;
            }

            if (parent is IAwaitOperation awaitOp && awaitOp.Parent is IReturnOperation)
            {
                return true;
            }

            return false;
        }

        private void TryHandleServiceInvocation(IInvocationOperation invocation)
        {
            if (_parameterTypes.Count == 0)
            {
                return;
            }

            if (invocation.Syntax is not InvocationExpressionSyntax invocationSyntax)
            {
                return;
            }

            if (invocationSyntax.Expression is not MemberAccessExpressionSyntax access)
            {
                return;
            }

            if (_analyzer.TryHandleServiceInvocationSyntax(_action, invocationSyntax, access, _parameterTypes, _fieldLookup, _project))
            {
                return;
            }

            var accessOperation = Model.GetOperation(access.Expression);
            var resolvedType = _analyzer.TryResolveExpressionType(
                accessOperation,
                _parameterTypes,
                _action.LocalVariables,
                _project.AssemblyName,
                _project.RelativeDirectory,
                _fieldLookup);

            if (string.IsNullOrWhiteSpace(resolvedType))
            {
                return;
            }

            var tree = invocationSyntax.SyntaxTree;
            _analyzer.HandleServiceInvocation(_action, access, invocationSyntax, resolvedType!, _parameterTypes, tree, _fieldLookup);
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
            _analyzer.EnsureHandlerAnalysis(requestType);
            var invocationName = invocation.TargetMethod?.Name;
            _analyzer.RecordControllerRequestFact(_action, requestType, invocationName, line);
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
            _analyzer.RecordControllerNotificationFact(_action, notificationType, line);
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
            _analyzer.RecordControllerMappingFact(_action, sourceType, destinationType!, null, line);
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
            _analyzer.RecordControllerHttpClientFact(_action, clientType, verb, route, methodName, line);
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
                    var noQuery = literal!;
                    var q = noQuery.IndexOf('?', StringComparison.Ordinal);
                    if (q >= 0) noQuery = noQuery[..q];
                    return noQuery;
                }
            }

            if (invocation.Arguments.Length > 0)
            {
                var literal = TryGetStringLiteral(invocation.Arguments[0].Value) ??
                              ValueContent.TryGetStringValue(invocation.Arguments[0].Value);
                if (!string.IsNullOrWhiteSpace(literal))
                {
                    var noQuery = literal!;
                    var q = noQuery.IndexOf('?', StringComparison.Ordinal);
                    if (q >= 0) noQuery = noQuery[..q];
                    return noQuery;
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
