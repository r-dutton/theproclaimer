using System.Collections.Generic;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
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
            FlowValueContentFacade valueContent)
            : base(model.Compilation, model, pointsTo, valueContent)
        {
            _analyzer = analyzer;
            _project = project;
            _profileFqdn = profileFqdn;
            _profileId = profileId;
            _profileFile = profileFile;
            _profileSpan = profileSpan;
            _registeredMappings = registeredMappings;
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            if (IsCreateMapInvocation(op))
            {
                HandleCreateMap(op);
            }

            base.VisitInvocation(op);
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
