using System;
using Microsoft.CodeAnalysis;

namespace GraphKit.Resolution
{
    internal static class SymbolExtensions
    {
        public static bool IsOrImplements(this INamedTypeSymbol? type, INamedTypeSymbol? iface)
        {
            if (type is null || iface is null) return false;
            if (SymbolEqualityComparer.Default.Equals(type, iface)) return true;
            foreach (var implemented in type.AllInterfaces)
            {
                if (SymbolEqualityComparer.Default.Equals(implemented.OriginalDefinition, iface.OriginalDefinition))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOrDerivedFrom(this INamedTypeSymbol? type, INamedTypeSymbol? baseType)
        {
            if (type is null || baseType is null) return false;
            if (SymbolEqualityComparer.Default.Equals(type, baseType)) return true;
            var current = type.BaseType;
            while (current is not null)
            {
                if (SymbolEqualityComparer.Default.Equals(current.OriginalDefinition, baseType.OriginalDefinition))
                {
                    return true;
                }
                current = current.BaseType;
            }
            return false;
        }

        public static string MetadataName(this INamedTypeSymbol symbol)
            => symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
    }
}

