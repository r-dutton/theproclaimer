using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis;
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

        public ServiceOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            string assembly,
            string project,
            string ownerMethod,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            ServiceInfo service)
            : base(model.Compilation, model, pointsTo, valueContent)
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
            if (AnalysisPredicates.IsValidatorCall(op))
            {
                HandleValidatorCall(op);
                base.VisitInvocation(op);
                return;
            }

            var receiver = op.Instance?.Type ?? op.TargetMethod.ContainingType;
            if (receiver is not null)
            {
                ProcessInvocation(op, receiver);
            }

            base.VisitInvocation(op);
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

        private void ProcessInvocation(IInvocationOperation invocation, ITypeSymbol receiver)
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
            var pointedTypes = invocation.Instance is not null
                ? PointsTo.TryGetLocationTypes(invocation.Instance)
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

            var serviceKey = $"{serviceTypeName}@{methodName}@{line}";
            if (_seenServices.Add(serviceKey))
            {
                _service.ServiceUsages.Add(new ServiceUsage(
                    serviceTypeName,
                    line,
                    _ownerMethod,
                    methodName,
                    ImplementationTypes: implementations));
            }
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
