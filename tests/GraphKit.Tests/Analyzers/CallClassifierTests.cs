using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraphKit.Classification;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Xunit;

namespace GraphKit.Tests.Analyzers;

public sealed class CallClassifierTests
{
    private static (CSharpCompilation Compilation, SemanticModel Model, IInvocationOperation Invocation) CompileAndGetInvocation(
        string source,
        string methodName)
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);
        var filePath = Path.Combine(tempRoot, "Sample.cs");

        var tree = CSharpSyntaxTree.ParseText(source, path: filePath);
        var compilation = CSharpCompilation.Create(
            assemblyName: "SampleAssembly",
            syntaxTrees: new[] { tree },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Task).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var model = compilation.GetSemanticModel(tree);
        var invocationSyntax = tree.GetRoot()
            .DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .Single(invocation =>
                invocation.Expression is MemberAccessExpressionSyntax member &&
                member.Name.Identifier.Text == methodName);

        var invocation = (IInvocationOperation)model.GetOperation(invocationSyntax)!;
        return (compilation, model, invocation);
    }

    [Fact]
    public void Classify_MediatorSend_ReturnsMediatorSend()
    {
        const string source = @"namespace MediatR
{
    public interface IMediator
    {
        System.Threading.Tasks.Task Send(object request);
    }
}

namespace Sample
{
    public sealed class Request { }

    public sealed class Service
    {
        private readonly MediatR.IMediator _mediator;

        public Service(MediatR.IMediator mediator) => _mediator = mediator;

        public void Execute()
        {
            _mediator.Send(new Request());
        }
    }
}";

        var (_, _, invocation) = CompileAndGetInvocation(source, "Send");
        var classifier = new CallClassifier();

        var kind = classifier.Classify(invocation);

        Assert.Equal(CallKind.MediatorSend, kind);
    }

    [Fact]
    public void Classify_RepositoryCall_ReturnsRepository()
    {
        const string source = @"namespace Sample
{
    public sealed class Entity { }

    public interface IRepository<T>
    {
        void Add(T entity);
    }

    public sealed class Repository : IRepository<Entity>
    {
        public void Add(Entity entity) { }
    }

    public sealed class Service
    {
        private readonly IRepository<Entity> _repository;

        public Service(IRepository<Entity> repository) => _repository = repository;

        public void Execute(Entity entity)
        {
            _repository.Add(entity);
        }
    }
}";

        var (_, _, invocation) = CompileAndGetInvocation(source, "Add");
        var classifier = new CallClassifier();

        var kind = classifier.Classify(invocation);

        Assert.Equal(CallKind.Repository, kind);
    }

    [Fact]
    public void Classify_HttpClientCall_ReturnsHttp()
    {
        const string source = @"namespace Sample
{
    public sealed class MyHttpClient
    {
        public void GetAsync(string uri) { }
    }

    public sealed class Service
    {
        private readonly MyHttpClient _client;

        public Service(MyHttpClient client) => _client = client;

        public void Execute()
        {
            _client.GetAsync(""/api/test"");
        }
    }
}";

        var (_, _, invocation) = CompileAndGetInvocation(source, "GetAsync");
        var classifier = new CallClassifier();

        var kind = classifier.Classify(invocation);

        Assert.Equal(CallKind.Http, kind);
    }
}
