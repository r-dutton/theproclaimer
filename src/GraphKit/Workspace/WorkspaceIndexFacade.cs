using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace GraphKit.Workspace
{
    public sealed class WorkspaceIndexFacade
    {
        // TODO: wrap your existing FlowWorkspaceIndex with typed methods
        public IEnumerable<IMethodSymbol> FindControllerActions() => Array.Empty<IMethodSymbol>();
        public IEnumerable<(string verb, string route, IMethodSymbol action)> Endpoints() => Array.Empty<(string,string,IMethodSymbol)>();
        public IEnumerable<(ITypeSymbol request, INamedTypeSymbol handler)> CqrsHandlers() => Array.Empty<(ITypeSymbol, INamedTypeSymbol)>();
    }
}
