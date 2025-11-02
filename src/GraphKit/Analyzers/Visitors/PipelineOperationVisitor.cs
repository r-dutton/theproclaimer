using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class PipelineOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly string _assembly;
        private readonly string _project;
        private readonly string _ownerMethod;
        private readonly List<ServiceUsage> _serviceUsages;
        private readonly List<OptionsUsage> _optionsUsages;
        private readonly List<CacheInvocation> _cacheInvocations;
        private readonly HashSet<string> _seenServices;
        private readonly HashSet<string> _seenOptions;
        private readonly HashSet<string> _seenCaches;

        public PipelineOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            string assembly,
            string project,
            string ownerMethod,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            List<ServiceUsage> serviceUsages,
            List<OptionsUsage> optionsUsages,
            List<CacheInvocation> cacheInvocations)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _assembly = assembly;
            _project = project;
            _ownerMethod = ownerMethod;
            _serviceUsages = serviceUsages;
            _optionsUsages = optionsUsages;
            _cacheInvocations = cacheInvocations;
            _seenServices = new HashSet<string>(
                serviceUsages.Select(s => $"{s.ServiceType}@{s.InvocationMethod}@{s.Line}"),
                StringComparer.OrdinalIgnoreCase);
            _seenOptions = new HashSet<string>(
                optionsUsages.Select(o => $"{o.OptionsType}@{o.Line}"),
                StringComparer.OrdinalIgnoreCase);
            _seenCaches = new HashSet<string>(
                cacheInvocations.Select(c => $"{c.CacheType}@{c.Method}@{c.Line}"),
                StringComparer.OrdinalIgnoreCase);
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            var receiver = op.Instance?.Type ?? op.TargetMethod.ContainingType;
            if (receiver is not null)
            {
                ProcessInvocation(op, receiver);
            }

            base.VisitInvocation(op);
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

            if (IsCacheService(qualified!))
            {
                var cacheKey = $"{qualified}@{methodName}@{line}";
                if (!_seenCaches.Add(cacheKey))
                {
                    return;
                }

                var operation = DetermineCacheOperation(methodName);
                _cacheInvocations.Add(new CacheInvocation(qualified!, methodName, null, line, operation));
                return;
            }

            if (TryResolveOptionsType(qualified!) is { } optionsType)
            {
                var optionKey = $"{optionsType}@{line}";
                if (_seenOptions.Add(optionKey))
                {
                    _optionsUsages.Add(new OptionsUsage(optionsType, line));
                }

                return;
            }

            var serviceKey = $"{qualified}@{methodName}@{line}";
            if (_seenServices.Add(serviceKey))
            {
                _serviceUsages.Add(new ServiceUsage(
                    qualified!,
                    line,
                    _ownerMethod,
                    methodName));
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
