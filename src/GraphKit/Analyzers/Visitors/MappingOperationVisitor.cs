using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class MappingOperationVisitor : FlowDataFlowOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly ProjectInfo _project;
        private readonly string _profileFqdn;
        private readonly string _profileId;
        private readonly string _profileFile;
        private readonly GraphSpan _profileSpan;
        private readonly HashSet<string> _registeredMappings;
        private readonly FactWriter _facts;
        private readonly ControlFlowTraversalState _flowState = new();

        public MappingOperationVisitor(
            ProjectAnalyzer analyzer,
            SemanticModel model,
            ProjectInfo project,
            string profileFqdn,
            string profileId,
            string profileFile,
            GraphSpan profileSpan,
            HashSet<string> registeredMappings,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent,
            FactWriter facts)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _project = project;
            _profileFqdn = profileFqdn;
            _profileId = profileId;
            _profileFile = profileFile;
            _profileSpan = profileSpan;
            _registeredMappings = registeredMappings;
            _facts = facts ?? throw new ArgumentNullException(nameof(facts));
            _ = _facts;
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            if (ShouldSkipOperation())
            {
                base.VisitInvocation(op);
                return;
            }

            if (IsCreateMapInvocation(op))
            {
                HandleCreateMap(op);
            }

            base.VisitInvocation(op);
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

        private bool IsCreateMapInvocation(IInvocationOperation invocation)
        {
            if (!string.Equals(invocation.TargetMethod.Name, "CreateMap", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!invocation.TargetMethod.IsGenericMethod)
            {
                return false;
            }

            return invocation.TargetMethod.TypeArguments.Length >= 2;
        }

        private void HandleCreateMap(IInvocationOperation invocation)
        {
            var typeArguments = invocation.TargetMethod.TypeArguments;
            if (typeArguments.Length < 2)
            {
                return;
            }

            var source = Qualify(typeArguments[0]);
            var destination = Qualify(typeArguments[1]);

            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(destination))
            {
                return;
            }

            var line = GetInvocationLine(invocation);
            _analyzer.RegisterMappingDefinition(
                _project,
                _profileFqdn,
                _profileId,
                _profileFile,
                _profileSpan,
                source!,
                destination!,
                line,
                _registeredMappings);
        }

        private string? Qualify(ITypeSymbol symbol)
        {
            var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            return _analyzer.QualifyTypeName(display, _project.AssemblyName, _project.RelativeDirectory) ?? display;
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
