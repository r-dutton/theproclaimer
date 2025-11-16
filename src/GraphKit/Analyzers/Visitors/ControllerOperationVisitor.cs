using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Facts;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Workspace;
using GraphKit.Classification;
using GraphKit.Http;
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
            private readonly HashSet<string> _seenLoggerUsages = new(StringComparer.OrdinalIgnoreCase);
            private readonly FactWriter _facts;
            private readonly ProjectInfo _project;
            private readonly IReadOnlyDictionary<string, string?> _parameterTypes;
            private readonly IReadOnlyDictionary<string, FieldDescriptor> _fieldLookup;
            private readonly CallClassifier _callClassifier;

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
            _callClassifier = new CallClassifier();
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
            var kind = _callClassifier.Classify(op);

            switch (kind)
            {
                case CallKind.MediatorSend:
                    HandleMediatorSend(op);
                    break;
                case CallKind.MediatorPublish:
                case CallKind.DomainEventPublish:
                    HandleMediatorPublish(op);
                    break;
                case CallKind.Mapper:
                    HandleMapperMap(op);
                    break;
                case CallKind.Http:
                    HandleHttpClientCall(op);
                    break;
            }

            HandleStatusCodeInvocation(op);
            TryHandleServiceInvocation(op);
            TryHandleHelperInvocation(op);
            TryHandleLoggerInvocation(op);

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
            if (IsRepositoryQueryInvocation(invocation))
            {
                return;
            }

            if (IsRepositoryExtension(invocation.TargetMethod))
            {
                return;
            }

            var clientSymbol = invocation.Instance?.Type
                               ?? invocation.Arguments.FirstOrDefault()?.Value.Type
                               ?? TryGetReceiverSymbol(invocation)
                               ?? invocation.TargetMethod.ContainingType;
            var clientType = Qualify(clientSymbol) ??
                             clientSymbol?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat) ??
                             "System.Net.Http.HttpClient";
            var methodName = invocation.TargetMethod.Name;

            if (string.IsNullOrWhiteSpace(clientType))
            {
                return;
            }

            if (LooksLikeDomainType(clientSymbol, clientType))
            {
                return;
            }

            if (IsCacheService(clientType!))
            {
                return;
            }

            if (string.Equals(methodName, "GetByIdAsync", StringComparison.OrdinalIgnoreCase) &&
                LooksLikeEntityLabel(clientType))
            {
                return;
            }

            string? route = null;
            string verb;
            if (RouteCanonicalizer.TryReconstruct(invocation, ValueContent, out var canonicalVerb, out var rawRoute))
            {
                route = NormalizeRoute(rawRoute);
                verb = canonicalVerb;
            }
            else
            {
                // Preserve previous behavior when reconstruction fails.
                verb = NormalizeHttpVerb(methodName) ?? methodName.ToUpperInvariant();
                route = TryResolveRoute(invocation);
                if (!string.IsNullOrWhiteSpace(route))
                {
                    route = NormalizeRoute(route!);
                }
            }

            if (string.IsNullOrWhiteSpace(route) &&
                (LooksLikeDomainType(clientSymbol, clientType) || LooksLikeEntityLabel(clientType)))
            {
                return;
            }

            if (string.Equals(methodName, "GetByIdAsync", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(route, "/id", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var line = GetInvocationLine(invocation);
            if (!string.IsNullOrWhiteSpace(route) &&
                (LooksLikeDomainType(clientSymbol, clientType) || LooksLikeEntityLabel(clientType)))
            {
                var trimmedRoute = route!.Trim('/');
                foreach (var argument in invocation.Arguments)
                {
                    if (argument.Parameter is { } parameter &&
                        string.Equals(trimmedRoute, parameter.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }
                }

                if (trimmedRoute.IndexOf('/') < 0 &&
                    trimmedRoute.IndexOf(':') < 0 &&
                    trimmedRoute.IndexOf('{') < 0)
                {
                    return;
                }
            }

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

                var literal = TryGetStringLiteral(argument.Value);
                if (string.IsNullOrWhiteSpace(literal))
                {
                    literal = ValueContent.DescribeStringValue(argument.Value).FirstNonEmptyLiteralOrDefault;
                }
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
                var literal = TryGetStringLiteral(invocation.Arguments[0].Value);
                if (string.IsNullOrWhiteSpace(literal))
                {
                    literal = ValueContent.DescribeStringValue(invocation.Arguments[0].Value).FirstNonEmptyLiteralOrDefault;
                }
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

        private void TryHandleLoggerInvocation(IInvocationOperation invocation)
        {
            if (invocation.TargetMethod is not { } method)
            {
                return;
            }

            var candidateType = invocation.Instance?.Type ?? method.ContainingType;
            if (candidateType is null)
            {
                return;
            }

            var qualified = candidateType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (!IsLoggerType(qualified))
            {
                return;
            }

            var line = GetInvocationLine(invocation);
            var invocationName = method.Name;
            if (_action.ServiceUsages.Any(s =>
                    string.Equals(s.ServiceType, qualified, StringComparison.OrdinalIgnoreCase) &&
                    s.Line == line &&
                    string.Equals(s.InvocationMethod ?? s.Method, invocationName, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            var key = $"{qualified}@{invocationName}@{line}";
            if (!_seenLoggerUsages.Add(key))
            {
                return;
            }

            _action.ServiceUsages.Add(new ServiceUsage(
                qualified,
                line,
                invocationName,
                invocationName));
        }

        private ITypeSymbol? TryGetReceiverSymbol(IInvocationOperation invocation)
        {
            if (invocation.Syntax is InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccess })
            {
                var receiver = memberAccess.Expression;
                var info = Model.GetTypeInfo(receiver);
                return info.Type ?? info.ConvertedType;
            }

            return null;
        }

        private static bool IsRepositoryQueryInvocation(IInvocationOperation invocation)
        {
            if (invocation.Syntax is not InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccess })
            {
                return false;
            }

            if (memberAccess.Expression is InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax innerAccess })
            {
                var name = innerAccess.Name.Identifier.Text;
                if (string.Equals(name, "ReadQuery", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(name, "WriteQuery", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool LooksLikeEntityLabel(string? typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return false;
            }

            var trimmed = typeName!;
            var lastDot = trimmed.LastIndexOf('.');
            if (lastDot >= 0)
            {
                trimmed = trimmed[(lastDot + 1)..];
            }

            if (trimmed.IndexOf("Client", StringComparison.OrdinalIgnoreCase) >= 0 ||
                trimmed.IndexOf("Service", StringComparison.OrdinalIgnoreCase) >= 0 ||
                trimmed.IndexOf("Proxy", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return false;
            }

            return true;
        }

        private static bool LooksLikeDomainType(ITypeSymbol? symbol, string? typeName)
        {
            if (symbol is not null)
            {
                var ns = symbol.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (!string.IsNullOrWhiteSpace(ns) && ns.IndexOf(".Domain", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }

                var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (display.IndexOf(".Domain", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }

                if (symbol is INamedTypeSymbol named && named.IsGenericType)
                {
                    foreach (var typeArgument in named.TypeArguments)
                    {
                        var argumentDisplay = typeArgument.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                        if (LooksLikeDomainType(typeArgument, argumentDisplay))
                        {
                            return true;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(typeName))
            {
                if (typeName!.IndexOf(".DomainModel.", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    typeName.IndexOf(".Domain.", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsRepositoryExtension(IMethodSymbol method)
        {
            if (method is null)
            {
                return false;
            }

            var candidate = method.ReducedFrom ?? method;

            var container = candidate.ContainingType?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (!string.IsNullOrWhiteSpace(container) &&
                container.IndexOf(".Data.Extensions.", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            var ns = candidate.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (!string.IsNullOrWhiteSpace(ns) &&
                ns.IndexOf(".Data.Extensions.", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
