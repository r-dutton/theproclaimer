using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Http;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class ServiceOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly string _assembly;
        private readonly string _project;
        private readonly string _ownerMethod;
        private readonly ServiceInfo _service;
        private readonly HashSet<string> _seenServices;
        private readonly HashSet<string> _seenOptions;
        private readonly HashSet<string> _seenCaches;
        private readonly HashSet<string> _seenValidators;
        private readonly HashSet<string> _seenLogs;
        private readonly ControlFlowTraversalState _flowState = new();

        public ServiceOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            string assembly,
            string project,
            string ownerMethod,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            FlowNullAnalysisFacade nullAnalysis,
            FlowCopyAnalysisFacade copyAnalysis,
            FlowPredicateAnalysisFacade predicateAnalysis,
            FlowTaintedDataFacade taintedData,
            ServiceInfo service)
            : base(model.Compilation, model, pointsTo, valueContent, nullAnalysis, copyAnalysis, predicateAnalysis, taintedData)
        {
            _analyzer = analyzer;
            _assembly = assembly;
            _project = project;
            _ownerMethod = ownerMethod;
            _service = service;
            _seenServices = new HashSet<string>(
                service.ServiceUsages.Select(s => $"{s.ServiceType}@{s.InvocationMethod}@{s.Line}"),
                StringComparer.OrdinalIgnoreCase);
            _seenOptions = new HashSet<string>(
                service.OptionsUsages.Select(o => $"{o.OptionsType}@{o.Line}"),
                StringComparer.OrdinalIgnoreCase);
            _seenCaches = new HashSet<string>(
                service.CacheInvocations.Select(c => $"{c.CacheType}@{c.Method}@{c.Line}"),
                StringComparer.OrdinalIgnoreCase);
            _seenValidators = new HashSet<string>(
                service.ValidationCalls.Select(v => $"{v.GuardType}@{v.Method}@{v.Line}"),
                StringComparer.OrdinalIgnoreCase);
            _seenLogs = new HashSet<string>(
                service.LogInvocations.Select(l => $"{l.Level}@{l.Line}"),
                StringComparer.OrdinalIgnoreCase);

        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            if (ShouldSkipOperation())
            {
                base.VisitInvocation(op);
                return;
            }

            if (AnalysisPredicates.IsValidatorCall(op))
            {
                HandleValidatorCall(op);
                base.VisitInvocation(op);
                return;
            }

            var instance = op.Instance;
            if (instance is null && op.TargetMethod.IsExtensionMethod && op.Arguments.Length > 0)
            {
                instance = op.Arguments[0].Value;
            }

            var receiver = instance?.Type ?? op.TargetMethod.ContainingType;
            if (receiver is not null)
            {
                ProcessInvocation(op, receiver, instance);
                TryHandleHttpInvocation(op, receiver);
                TryHandleHttpWrapper(op);
            }

            base.VisitInvocation(op);
        }

        public override void Visit(IOperation op)
        {
            if (ShouldSkipOperation())
            {
                base.Visit(op);
                return;
            }

            switch (op)
            {
                case IFieldReferenceOperation fieldReference:
                    var declaringType = Qualify(fieldReference.Field.ContainingType);
                    if (!string.IsNullOrWhiteSpace(declaringType) &&
                        string.Equals(declaringType, _service.Fqdn, StringComparison.OrdinalIgnoreCase) &&
                        fieldReference.Parent is not IPropertyReferenceOperation &&
                        fieldReference.Parent is not IInvocationOperation)
                    {
                        ProcessFieldReference(fieldReference);
                    }

                    break;
                case IPropertyReferenceOperation property:
                    if (property.Instance is IFieldReferenceOperation propertyField)
                    {
                        var fieldOwner = Qualify(propertyField.Field.ContainingType);
                        if (!string.IsNullOrWhiteSpace(fieldOwner) &&
                            string.Equals(fieldOwner, _service.Fqdn, StringComparison.OrdinalIgnoreCase))
                        {
                            ProcessFieldPropertyReference(property, propertyField);
                        }
                    }
                    else
                    {
                        ProcessPropertyReference(property);
                    }

                    break;
            }

            base.Visit(op);
        }

        protected override void OnBranch(ControlFlowBranch branch, IOperation? condition)
        {
            _flowState.OnBranch(branch, condition);
        }

        protected override void OnEnterRegion(ControlFlowRegion region)
        {
            _flowState.OnEnterRegion(region);
        }

        protected override void OnLeaveRegion(ControlFlowRegion region)
        {
            _flowState.OnLeaveRegion(region);
        }

        private bool ShouldSkipOperation()
        {
            return _flowState.ShouldSkip(CurrentBlock);
        }

        private void HandleValidatorCall(IInvocationOperation invocation)
        {
            var validatorType = Qualify(invocation.Instance?.Type ?? invocation.TargetMethod.ContainingType);
            if (string.IsNullOrWhiteSpace(validatorType))
            {
                return;
            }

            var methodName = invocation.TargetMethod?.Name ?? string.Empty;
            var line = GetInvocationLine(invocation);
            var key = $"{validatorType}@{methodName}@{line}";
            if (!_seenValidators.Add(key))
            {
                return;
            }

            _service.ValidationCalls.Add(new HandlerValidationCall(validatorType!, methodName, line));
        }

        private void ProcessInvocation(IInvocationOperation invocation, ITypeSymbol receiver, IOperation? serviceInstance)
        {
            var qualified = Qualify(receiver);
            if (string.IsNullOrWhiteSpace(qualified))
            {
                return;
            }

            var methodName = invocation.TargetMethod?.Name ?? string.Empty;
            var line = GetInvocationLine(invocation);
            var simple = GetTypeNameWithoutGenerics(qualified!);

            if (IsLoggerType(simple))
            {
                if (TryExtractLogLevel(methodName) is { } level)
                {
                    var logKey = $"{level}@{line}";
                    if (_seenLogs.Add(logKey))
                    {
                        _service.LogInvocations.Add(new HandlerLogInvocation(level, line));
                    }
                }

                return;
            }

            if (IsCacheService(qualified!))
            {
                var cacheKey = $"{qualified}@{methodName}@{line}";
                if (!_seenCaches.Add(cacheKey))
                {
                    return;
                }

                var operation = DetermineCacheOperation(methodName);
                _service.CacheInvocations.Add(new CacheInvocation(qualified!, methodName, null, line, operation));
                return;
            }

            if (TryResolveOptionsType(qualified!) is { } optionsType)
            {
                var optionKey = $"{optionsType}@{line}";
                if (_seenOptions.Add(optionKey))
                {
                    _service.OptionsUsages.Add(new OptionsUsage(optionsType, line));
                }

                return;
            }

            IReadOnlyCollection<string>? implementations = null;
            var pointedTypes = serviceInstance is not null
                ? PointsTo.TryGetLocationTypes(serviceInstance)
                : ImmutableArray<ITypeSymbol>.Empty;

            if (!pointedTypes.IsDefaultOrEmpty)
            {
                var unique = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var candidate in pointedTypes)
                {
                    if (Qualify(candidate) is { } resolved && unique.Add(resolved))
                    {
                        continue;
                    }
                }

                if (unique.Count > 0)
                {
                    implementations = unique.ToList();
                }
            }

            var serviceTypeName = qualified!;
            var preferredService = SelectPreferredServiceType(serviceTypeName, implementations);

            string? dispatchTarget = null;
            string? requestType = null;
            string? responseType = null;
            string? dispatchKind = null;

            if (_analyzer.TryResolveRequestDispatch(
                    PointsTo,
                    invocation,
                    qualified!,
                    implementations,
                    _assembly,
                    _project,
                    out var target,
                    out var request,
                    out var response,
                    out var kind))
            {
                dispatchTarget = target;
                requestType = request;
                responseType = response;
                dispatchKind = kind;
            }

            RecordServiceUsage(
                preferredService,
                methodName,
                line,
                implementations,
                dispatchTarget,
                requestType,
                responseType,
                dispatchKind);
        }

        private void ProcessFieldPropertyReference(IPropertyReferenceOperation property, IFieldReferenceOperation fieldReference)
        {
            var receiver = fieldReference.Field.Type;
            var qualified = Qualify(receiver);
            if (string.IsNullOrWhiteSpace(qualified))
            {
                return;
            }

            var propertyName = property.Property?.Name ?? property.Member.Name;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            var line = GetPropertyLine(property);
            var implementations = CollectImplementationCandidates(qualified!, fieldReference);

            var serviceTypeName = qualified!;
            if (implementations is { Count: > 0 })
            {
                serviceTypeName = SelectPreferredServiceType(serviceTypeName, implementations);
            }

            RecordServiceUsage(serviceTypeName, propertyName, line, implementations);
        }

        private void ProcessFieldReference(IFieldReferenceOperation fieldReference)
        {
            var receiver = fieldReference.Field.Type;
            var qualified = Qualify(receiver);
            if (string.IsNullOrWhiteSpace(qualified))
            {
                return;
            }

            var fieldName = fieldReference.Field.Name;
            var line = GetFieldLine(fieldReference);
            var implementations = CollectImplementationCandidates(qualified!, fieldReference);

            var serviceTypeName = qualified!;
            if (implementations is { Count: > 0 })
            {
                serviceTypeName = SelectPreferredServiceType(serviceTypeName, implementations);
            }

            RecordServiceUsage(serviceTypeName, fieldName, line, implementations);
        }

        private void ProcessPropertyReference(IPropertyReferenceOperation property)
        {
            if (property.Instance is null)
            {
                return;
            }

            var propertyName = property.Property?.Name ?? property.Member.Name;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            if (TryResolveAliasedField(property.Instance, out var aliasedField))
            {
                var aliasType = Qualify(aliasedField?.Type);
                if (!string.IsNullOrWhiteSpace(aliasType))
                {
                    var line = GetPropertyLine(property);
                    RecordServiceUsage(aliasType!, propertyName, line);
                }

                return;
            }

            var instanceType = property.Instance.Type;
            if (instanceType is not null && !instanceType.IsReferenceType)
            {
                return;
            }

            var candidateType = Qualify(instanceType) ??
                                Qualify(property.Property?.ContainingType);
            if (string.IsNullOrWhiteSpace(candidateType) ||
                string.Equals(candidateType, _service.Fqdn, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var line = GetPropertyLine(property);
            var implementations = CollectImplementationCandidates(candidateType!, property.Instance);
            var preferred = SelectPreferredServiceType(candidateType!, implementations);

            RecordServiceUsage(preferred, propertyName, line, implementations);
        }

        private IReadOnlyCollection<string>? CollectImplementationCandidates(string serviceTypeName, IOperation operation)
        {
            var unique = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            unique.Add(serviceTypeName);

            var pointedTypes = PointsTo.TryGetLocationTypes(operation);
            if (!pointedTypes.IsDefaultOrEmpty)
            {
                foreach (var candidate in pointedTypes)
                {
                    if (Qualify(candidate) is { } resolved)
                    {
                        unique.Add(resolved);
                    }
                }
            }

            if (_analyzer.ResolveImplementationType(serviceTypeName, _assembly, _project) is { } resolvedImplementation)
            {
                unique.Add(resolvedImplementation);
            }

            return unique.Count > 0 ? unique.ToList() : null;
        }

        private string SelectPreferredServiceType(string fallback, IReadOnlyCollection<string>? implementations)
        {
            if (implementations is { Count: > 0 })
            {
                foreach (var implementation in implementations)
                {
                    if (_analyzer.TryResolveScopedService(implementation, _assembly, _project, out var scoped))
                    {
                        return scoped;
                    }
                }
            }

            return fallback;
        }

        private bool TryResolveAliasedField(IOperation instance, out IFieldSymbol? fieldSymbol)
        {
            fieldSymbol = null;
            if (CopyAnalysis is null)
            {
                return false;
            }

            foreach (var field in CopyAnalysis.GetReferencedFields(instance))
            {
                var owner = Qualify(field.ContainingType);
                if (!string.IsNullOrWhiteSpace(owner) &&
                    string.Equals(owner, _service.Fqdn, StringComparison.OrdinalIgnoreCase))
                {
                    fieldSymbol = field;
                    return true;
                }
            }

            return false;
        }

        private void RecordServiceUsage(
            string serviceTypeName,
            string invocationMember,
            int line,
            IReadOnlyCollection<string>? implementations = null,
            string? targetType = null,
            string? requestType = null,
            string? responseType = null,
            string? dispatchKind = null)
        {
            if (string.IsNullOrWhiteSpace(serviceTypeName) || string.IsNullOrWhiteSpace(invocationMember))
            {
                return;
            }

            if (string.Equals(serviceTypeName, _service.Fqdn, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var key = $"{serviceTypeName}@{invocationMember}@{line}";
            if (!_seenServices.Add(key))
            {
                return;
            }
            _service.ServiceUsages.Add(new ServiceUsage(
                serviceTypeName,
                line,
                Method: _ownerMethod,
                InvocationMethod: invocationMember,
                RequestType: requestType,
                ResponseType: responseType,
                DispatchKind: dispatchKind,
                TargetType: targetType,
                ImplementationTypes: implementations));


            if (ProjectAnalyzer.IsFrameworkServiceType(serviceTypeName) &&
                !_service.FrameworkInteractions.Any(fi =>
                    string.Equals(fi.ServiceType, serviceTypeName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(fi.Member, invocationMember, StringComparison.OrdinalIgnoreCase) &&
                    fi.Line == line))
            {
                _service.FrameworkInteractions.Add(new FrameworkInteraction(serviceTypeName, invocationMember, line));
            }
        }

        private void TryHandleHttpInvocation(IInvocationOperation invocation, ITypeSymbol receiver)
        {
            if (PredicateAnalysis?.IsAlwaysFalse(invocation) ?? false)
            {
                return;
            }

            if (invocation.Instance is not null && (NullAnalysis?.IsDefinitelyNull(invocation.Instance) ?? false))
            {
                return;
            }

            if (!AnalysisPredicates.IsHttpClientCall(invocation))
            {
                return;
            }

            if (IsRepositoryExtension(invocation.TargetMethod))
            {
                return;
            }

            var clientType = Qualify(receiver);
            if (string.IsNullOrWhiteSpace(clientType))
            {
                return;
            }

            if (LooksLikeDomainType(receiver, clientType))
            {
                return;
            }

            if (IsCacheService(clientType!))
            {
                return;
            }

            string? route = null;
            string httpVerb;
            if (RouteCanonicalizer.TryReconstruct(invocation, ValueContent, out var canonicalVerb, out var rawRoute))
            {
                httpVerb = canonicalVerb;
                route = rawRoute;
            }
            else
            {
                var methodName = invocation.TargetMethod?.Name ?? string.Empty;
                httpVerb = ProjectAnalyzer.NormalizeHttpVerb(methodName) ?? methodName.ToUpperInvariant();
                route = invocation.Arguments.Length > 0
                    ? TryRenderValue(ValueContent, invocation.Arguments[0].Value)
                    : null;
            }

            var (normalizedRoute, parameters) = NormalizeRouteWithQuery(route);
            var line = GetInvocationLine(invocation);
            var containsTaint = TaintedData?.IsInvocationTainted(invocation) ?? false;

            _service.HttpClientInvocations.Add(new HandlerClientInvocation(
                clientType!,
                httpVerb,
                normalizedRoute,
                line,
                invocation.TargetMethod?.Name,
                null,
                parameters,
                _ownerMethod,
                containsTaint));
        }

        private void TryHandleHttpWrapper(IInvocationOperation invocation)
        {
            var methodName = invocation.TargetMethod?.Name;
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return;
            }

            if (PredicateAnalysis?.IsAlwaysFalse(invocation) ?? false)
            {
                return;
            }

            // Wrapper patterns: Request style or verb-prefixed helpers
            var isWrapper = BaseServiceInvocationNames.Contains(methodName!) || IsLikelyHttpWrapperName(methodName!);
            if (!isWrapper)
            {
                return;
            }

            string? httpVerb = ProjectAnalyzer.NormalizeHttpVerb(methodName);
            if (httpVerb is null && invocation.Arguments.Length > 0)
            {
                var verbCandidate = TryRenderValue(ValueContent, invocation.Arguments[0].Value);
                httpVerb = verbCandidate?.ToUpperInvariant();
            }

            string? route = null;
            foreach (var arg in invocation.Arguments)
            {
                if (IsRouteParameter(arg.Parameter))
                {
                    route = TryRenderValue(ValueContent, arg.Value);
                    if (!string.IsNullOrWhiteSpace(route)) break;
                }
            }
            route ??= invocation.Arguments.Length > 1
                ? TryRenderValue(ValueContent, invocation.Arguments[1].Value)
                : null;

            var (normalizedRoute, parameters) = NormalizeRouteWithQuery(route);
            var line = GetInvocationLine(invocation);
            var containsTaint = TaintedData?.IsInvocationTainted(invocation) ?? false;

            var baseTypeSymbol = invocation.TargetMethod?.ContainingType;
            var baseType = Qualify(baseTypeSymbol) ?? _service.Fqdn;
            var candidateClients = ExtractWrapperClientCandidates(invocation.TargetMethod);

            _service.BaseServiceClientInvocations.Add(new BaseServiceClientInvocation(
                baseType,
                _assembly,
                methodName!,
                httpVerb ?? string.Empty,
                normalizedRoute,
                parameters,
                line,
                _ownerMethod,
                candidateClients,
                containsTaint));
        }

        private static bool IsRouteParameter(IParameterSymbol? parameter)
        {
            if (parameter is null) return false;
            var name = parameter.Name;
            return name.Equals("requestUri", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("uri", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("url", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("endpoint", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("path", StringComparison.OrdinalIgnoreCase);
        }

        private static string? TryRenderValue(FlowValueContentFacade valueContent, IOperation? operation)
        {
            if (operation is null)
            {
                return null;
            }

            foreach (var candidate in valueContent.EnumerateContentCandidates(operation))
            {
                var literal = candidate.TryGetLiteralText();
                if (!string.IsNullOrWhiteSpace(literal))
                {
                    return literal;
                }

                var placeholder = candidate.ToDisplayString();
                if (!string.IsNullOrWhiteSpace(placeholder))
                {
                    return placeholder;
                }
            }

            return null;
        }

        private static (string? Route, IReadOnlyCollection<string>? QueryParameters) NormalizeRouteWithQuery(string? route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                return (null, null);
            }

            var normalized = ProjectAnalyzer.NormalizeRoute(route);
            var idx = normalized.IndexOf('?', StringComparison.Ordinal);
            if (idx < 0)
            {
                return (normalized, null);
            }

            var path = normalized[..idx];
            var query = normalized[(idx + 1)..];
            var parameters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var part in query.Split('&', StringSplitOptions.RemoveEmptyEntries))
            {
                var key = part.Split('=')[0];
                key = ProjectAnalyzer.NormalizeQueryKey(key);
                if (string.IsNullOrWhiteSpace(key)) key = "{*}";
                parameters.Add(key);
            }
            return (parameters.Count > 0 ? $"{path}?{string.Join("&", parameters.OrderBy(p => p, StringComparer.OrdinalIgnoreCase).Select(p => $"{p}={{*}}"))}" : path,
                    parameters.Count > 0 ? parameters.ToArray() : null);
        }

        private static bool IsLikelyHttpWrapperName(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName)) return false;
            if (methodName.Equals("Request", StringComparison.OrdinalIgnoreCase) ||
                methodName.Equals("SendRequest", StringComparison.OrdinalIgnoreCase) ||
                methodName.Equals("SendRequestAsync", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            foreach (var verb in new[] { "Get", "Post", "Put", "Delete", "Patch", "Head", "Options" })
            {
                if (methodName.StartsWith(verb, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        private void ProcessFieldPropertyReference(IPropertyReferenceOperation property, IFieldReferenceOperation fieldReference)
        {
            var receiver = fieldReference.Field.Type;
            var qualified = Qualify(receiver);
            if (string.IsNullOrWhiteSpace(qualified))
            {
                return;
            }

            var propertyName = property.Property?.Name ?? property.Member.Name;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            var line = GetPropertyLine(property);
            var implementations = CollectImplementationCandidates(qualified!, fieldReference);

            var serviceTypeName = qualified!;
            if (implementations is { Count: > 0 })
            {
                foreach (var implementation in implementations)
                {
                    if (_analyzer.TryResolveScopedService(implementation, _assembly, _project, out var scoped))
                    {
                        serviceTypeName = scoped;
                        break;
                    }
                }
            }

            var serviceKey = $"{serviceTypeName}@{propertyName}@{line}";
            if (!_seenServices.Add(serviceKey))
            {
                return;
            }

            _service.ServiceUsages.Add(new ServiceUsage(
                serviceTypeName,
                line,
                _ownerMethod,
                propertyName,
                ImplementationTypes: implementations));

            if (ProjectAnalyzer.IsFrameworkServiceType(serviceTypeName) &&
                !_service.FrameworkInteractions.Any(fi =>
                    string.Equals(fi.ServiceType, serviceTypeName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(fi.Member, propertyName, StringComparison.OrdinalIgnoreCase) &&
                    fi.Line == line))
            {
                _service.FrameworkInteractions.Add(new FrameworkInteraction(serviceTypeName, propertyName, line));
            }
        }

        private void ProcessFieldReference(IFieldReferenceOperation fieldReference)
        {
            var receiver = fieldReference.Field.Type;
            var qualified = Qualify(receiver);
            if (string.IsNullOrWhiteSpace(qualified))
            {
                return;
            }

            var fieldName = fieldReference.Field.Name;
            var line = GetFieldLine(fieldReference);
            var implementations = CollectImplementationCandidates(qualified!, fieldReference);

            var serviceTypeName = qualified!;
            if (implementations is { Count: > 0 })
            {
                foreach (var implementation in implementations)
                {
                    if (_analyzer.TryResolveScopedService(implementation, _assembly, _project, out var scoped))
                    {
                        serviceTypeName = scoped;
                        break;
                    }
                }
            }

            var serviceKey = $"{serviceTypeName}@{fieldName}@{line}";
            if (!_seenServices.Add(serviceKey))
            {
                return;
            }

            _service.ServiceUsages.Add(new ServiceUsage(
                serviceTypeName,
                line,
                _ownerMethod,
                fieldName,
                ImplementationTypes: implementations));

            if (ProjectAnalyzer.IsFrameworkServiceType(serviceTypeName) &&
                !_service.FrameworkInteractions.Any(fi =>
                    string.Equals(fi.ServiceType, serviceTypeName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(fi.Member, fieldName, StringComparison.OrdinalIgnoreCase) &&
                    fi.Line == line))
            {
                _service.FrameworkInteractions.Add(new FrameworkInteraction(serviceTypeName, fieldName, line));
            }
        }

        private IReadOnlyCollection<string>? CollectImplementationCandidates(string serviceTypeName, IOperation operation)
        {
            var unique = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            unique.Add(serviceTypeName);

            var pointedTypes = PointsTo.TryGetLocationTypes(operation);
            if (!pointedTypes.IsDefaultOrEmpty)
            {
                foreach (var candidate in pointedTypes)
                {
                    if (Qualify(candidate) is { } resolved)
                    {
                        unique.Add(resolved);
                    }
                }
            }

            if (_analyzer.ResolveImplementationType(serviceTypeName, _assembly, _project) is { } resolvedImplementation)
            {
                unique.Add(resolvedImplementation);
            }

            return unique.Count > 0 ? unique.ToList() : null;
        }

        private string? Qualify(ITypeSymbol? symbol)
        {
            if (symbol is null)
            {
                return null;
            }

            var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (!display.Contains('.') && symbol is INamedTypeSymbol named)
            {
                var containing = named.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? string.Empty;
                if (containing.StartsWith("global::", StringComparison.Ordinal))
                {
                    containing = containing["global::".Length..];
                }

                if (!string.IsNullOrWhiteSpace(containing))
                {
                    display = $"{containing}.{named.Name}";
                }
                else
                {
                    display = named.Name;
                }
            }

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

        private static int GetPropertyLine(IPropertyReferenceOperation property)
        {
            if (property.Syntax?.SyntaxTree is { } tree)
            {
                return GetLineNumber(tree, property.Syntax);
            }

            return 0;
        }

        private static int GetFieldLine(IFieldReferenceOperation fieldReference)
        {
            if (fieldReference.Syntax?.SyntaxTree is { } tree)
            {
                return GetLineNumber(tree, fieldReference.Syntax);
            }

            return 0;
        }

        private IReadOnlyCollection<string>? ExtractWrapperClientCandidates(IMethodSymbol? wrapperMethod)
        {
            if (wrapperMethod is null)
            {
                return null;
            }

            var collector = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var visited = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
            var stack = new Stack<ITypeSymbol?>();
            if (wrapperMethod.ContainingType is { } containing)
            {
                stack.Push(containing);
            }

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current is null || !visited.Add(current))
                {
                    continue;
                }

                foreach (var member in current.GetMembers())
                {
                    switch (member)
                    {
                        case IFieldSymbol field when !field.IsStatic:
                            TryAddClientCandidate(field.Type, collector);
                            break;
                        case IPropertySymbol property when !property.IsStatic:
                            TryAddClientCandidate(property.Type, collector);
                            break;
                    }
                }

                if (current.BaseType is not null)
                {
                    stack.Push(current.BaseType);
                }
            }

            return collector.Count > 0 ? collector.ToArray() : null;
        }

        private void TryAddClientCandidate(ITypeSymbol? candidateType, HashSet<string> collector)
        {
            if (candidateType is null)
            {
                return;
            }

            var qualified = Qualify(candidateType);
            if (string.IsNullOrWhiteSpace(qualified))
            {
                return;
            }

            if (!LooksLikeHttpClientCandidate(candidateType, qualified!))
            {
                return;
            }

            collector.Add(qualified!);
        }

        private static bool LooksLikeHttpClientCandidate(ITypeSymbol typeSymbol, string qualifiedName)
        {
            static bool NameMatches(string candidate)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    return false;
                }

                return candidate.IndexOf("HttpClient", StringComparison.OrdinalIgnoreCase) >= 0 ||
                       candidate.IndexOf("RestClient", StringComparison.OrdinalIgnoreCase) >= 0 ||
                       candidate.IndexOf("OAuthClient", StringComparison.OrdinalIgnoreCase) >= 0 ||
                       candidate.IndexOf("ApiClient", StringComparison.OrdinalIgnoreCase) >= 0 ||
                       candidate.EndsWith("Client", StringComparison.OrdinalIgnoreCase) ||
                       candidate.EndsWith("Proxy", StringComparison.OrdinalIgnoreCase);
            }

            if (NameMatches(qualifiedName))
            {
                return true;
            }

            if (typeSymbol is INamedTypeSymbol named)
            {
                foreach (var iface in named.AllInterfaces)
                {
                    var ifaceName = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                    if (NameMatches(ifaceName))
                    {
                        return true;
                    }
                }

                var current = named;
                while (current is not null && !string.Equals(current.Name, "Object", StringComparison.Ordinal))
                {
                    var currentDisplay = current.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                    if (NameMatches(currentDisplay))
                    {
                        return true;
                    }

                    current = current.BaseType;
                }
            }

            return false;
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

            var container = method.ContainingType?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (!string.IsNullOrWhiteSpace(container) &&
                container.IndexOf(".Data.Extensions.", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            var ns = method.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (!string.IsNullOrWhiteSpace(ns) &&
                ns.IndexOf(".Data.Extensions.", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
