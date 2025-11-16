using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraphKit.Analyzers;
using GraphKit.Constants;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Xunit;

namespace GraphKit.Tests.Analyzers;

public sealed class RequestProcessorDispatchTests
{
    [Fact]
    public void TryResolveRequestDispatch_IdentifiesRequestAndResponseTypes()
    {
        const string source = @"namespace Sample
{
    public interface IRequestProcessor<TRequest, TResponse>
    {
        System.Threading.Tasks.Task<TResponse> ProcessAsync(TRequest request);
    }

    public sealed class SampleRequest { }
    public sealed class SampleResponse { }

    public sealed class SampleService
    {
        private readonly IRequestProcessor<SampleRequest, SampleResponse> _processor;

        public SampleService(IRequestProcessor<SampleRequest, SampleResponse> processor)
        {
            _processor = processor;
        }

        public System.Threading.Tasks.Task<SampleResponse> ExecuteAsync()
        {
            return _processor.ProcessAsync(new SampleRequest());
        }
    }
}";

        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);
        var filePath = Path.Combine(tempRoot, "SampleService.cs");

        var tree = CSharpSyntaxTree.ParseText(source, path: filePath);
        var compilation = CSharpCompilation.Create(
            assemblyName: "SampleAssembly",
            syntaxTrees: new[] { tree },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Task).Assembly.Location)
            },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var projectInfo = new GraphKit.Workspace.ProjectInfo(
            projectPath: Path.Combine(tempRoot, "SampleAssembly.csproj"),
            assemblyName: compilation.AssemblyName!,
            rootNamespace: "Sample",
            relativeDirectory: tempRoot,
            sourceFiles: new[] { filePath },
            compilationFactory: () => compilation);

        var analyzer = new ProjectAnalyzer(tempRoot);
        var pointsToOptions = ProjectAnalyzer.ProjectAnalyzerConfiguration.Default.GetPointsToAnalysisOptions();
        var pointsTo = new FlowPointsToFacade(analyzer.InterproceduralConfiguration, _ => true, pointsToOptions);

        var model = compilation.GetSemanticModel(tree);
        var invocationSyntax = tree.GetRoot()
            .DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .Single(static invocation => invocation.Expression is MemberAccessExpressionSyntax member && member.Name.Identifier.Text == "ProcessAsync");

        var invocationOperation = (IInvocationOperation)model.GetOperation(invocationSyntax)!;
        var receiverTypeName = invocationOperation.Instance?.Type?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat) ?? string.Empty;

        var resolved = analyzer.TryResolveRequestDispatch(
            pointsTo,
            invocationOperation,
            receiverTypeName,
            implementations: null,
            assembly: compilation.AssemblyName ?? string.Empty,
            project: projectInfo.RelativeDirectory,
            targetType: out var targetType,
            requestType: out var requestType,
            responseType: out var responseType,
            dispatchKind: out var dispatchKind);

        Assert.True(resolved);
        Assert.Equal("Sample.SampleRequest", requestType);
        Assert.Equal("Sample.SampleRequest", targetType);
        Assert.Equal("Sample.SampleResponse", responseType);
        Assert.Equal(EdgeKinds.RequestProcessorDispatch, dispatchKind);
    }
}
