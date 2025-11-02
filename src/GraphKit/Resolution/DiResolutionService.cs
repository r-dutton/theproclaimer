using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace GraphKit.Resolution
{
    public sealed class DiResolutionService
    {
        // Interface/BaseType -> Implementations
        private readonly Dictionary<INamedTypeSymbol, HashSet<INamedTypeSymbol>> _serviceMap =
            new(SymbolEqualityComparer.Default);

        public void Register(INamedTypeSymbol service, INamedTypeSymbol impl)
        {
            if (!_serviceMap.TryGetValue(service, out var set))
            {
                set = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
                _serviceMap[service] = set;
            }
            set.Add(impl);
        }

        public IEnumerable<INamedTypeSymbol> ResolveService(INamedTypeSymbol service)
            => _serviceMap.TryGetValue(service, out var set) ? set : System.Array.Empty<INamedTypeSymbol>();

        public IMethodSymbol? ResolveImplementationMethod(IMethodSymbol ifaceMethod, INamedTypeSymbol implType)
        {
            foreach (var m in implType.GetMembers())
            {
                if (m is IMethodSymbol mm)
                {
                    foreach (var e in mm.ExplicitInterfaceImplementations)
                    {
                        if (SymbolEqualityComparer.Default.Equals(e, ifaceMethod)) return mm;
                    }
                    if (mm.OverriddenMethod is IMethodSymbol ov &&
                        SymbolEqualityComparer.Default.Equals(ov, ifaceMethod))
                        return mm;
                }
            }
            return null;
        }
    }
}
