using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace GraphKit.Resolution
{
    internal sealed class RoslynTypeIndex
    {
        private static readonly ConditionalWeakTable<Compilation, RoslynTypeIndex> Cache = new();

        public static RoslynTypeIndex Get(Compilation compilation)
            => Cache.GetValue(compilation, c => new RoslynTypeIndex(c));

        private RoslynTypeIndex(Compilation compilation)
        {
            Compilation = compilation;
            Task = Resolve("System.Threading.Tasks.Task");
            TaskOfT = Resolve("System.Threading.Tasks.Task`1");
            ValueTask = Resolve("System.Threading.Tasks.ValueTask");
            ValueTaskOfT = Resolve("System.Threading.Tasks.ValueTask`1");
            HttpClient = Resolve("System.Net.Http.HttpClient");
            IHttpClientFactory = Resolve("System.Net.Http.IHttpClientFactory");
            DbContext = Resolve("Microsoft.EntityFrameworkCore.DbContext");
            DbSet1 = Resolve("Microsoft.EntityFrameworkCore.DbSet`1");
            Mediator = Resolve("MediatR.IMediator");
            Sender = Resolve("MediatR.ISender");
            Publisher = Resolve("MediatR.IPublisher");
            IRequest1 = Resolve("MediatR.IRequest`1");
            IMapper = Resolve("AutoMapper.IMapper");
        }

        public Compilation Compilation { get; }

        public INamedTypeSymbol? Task { get; }
        public INamedTypeSymbol? TaskOfT { get; }
        public INamedTypeSymbol? ValueTask { get; }
        public INamedTypeSymbol? ValueTaskOfT { get; }
        public INamedTypeSymbol? HttpClient { get; }
        public INamedTypeSymbol? IHttpClientFactory { get; }
        public INamedTypeSymbol? DbContext { get; }
        public INamedTypeSymbol? DbSet1 { get; }
        public INamedTypeSymbol? Mediator { get; }
        public INamedTypeSymbol? Sender { get; }
        public INamedTypeSymbol? Publisher { get; }
        public INamedTypeSymbol? IRequest1 { get; }
        public INamedTypeSymbol? IMapper { get; }

        private INamedTypeSymbol? Resolve(string metadataName)
            => Compilation.GetTypeByMetadataName(metadataName);

        public bool IsTaskLike(ITypeSymbol? type)
        {
            if (type is not INamedTypeSymbol named)
            {
                return false;
            }

            var def = named.OriginalDefinition;
            return SymbolEqualityComparer.Default.Equals(def, Task) ||
                   SymbolEqualityComparer.Default.Equals(def, TaskOfT) ||
                   SymbolEqualityComparer.Default.Equals(def, ValueTask) ||
                   SymbolEqualityComparer.Default.Equals(def, ValueTaskOfT);
        }
    }
}

