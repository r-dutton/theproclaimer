using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Ef
{
    public static class EfRecognizer
    {
        private const string DbContextMeta = "Microsoft.EntityFrameworkCore.DbContext";
        private const string DbSetMeta = "Microsoft.EntityFrameworkCore.DbSet`1";

        public static bool IsDbContext(ITypeSymbol t)
        {
            if (t is INamedTypeSymbol named)
            {
                var cur = named;
                while (cur is not null)
                {
                    if (ToMeta(cur).Equals(DbContextMeta, System.StringComparison.Ordinal))
                    {
                        return true;
                    }

                    cur = cur.BaseType;
                }
            }

            return false;
        }

        public static bool IsSaveChanges(IMethodSymbol m) => m.Name is "SaveChanges" or "SaveChangesAsync";
        public static bool IsWrite(IMethodSymbol m) => m.Name is "Add" or "AddAsync" or "Update" or "Remove";

        public static ITypeSymbol? TryGetDbSetEntity(IOperation op)
        {
            switch (op)
            {
                case IPropertyReferenceOperation prop when prop.Property.Type is INamedTypeSymbol nts:
                    if (IsDbSetOpenGeneric(nts) && nts.TypeArguments.Length == 1)
                    {
                        return nts.TypeArguments[0];
                    }
                    break;
                case IInvocationOperation inv:
                    if (inv.TargetMethod.IsGenericMethod && inv.TargetMethod.Name == "Set" && inv.TargetMethod.TypeArguments.Length == 1)
                    {
                        return inv.TargetMethod.TypeArguments[0];
                    }
                    break;
            }
            return null;
        }

        private static bool IsDbSetOpenGeneric(INamedTypeSymbol type)
        {
            var od = type.OriginalDefinition;
            if (od is null)
            {
                return false;
            }

            return ToMeta(od).Equals(DbSetMeta, System.StringComparison.Ordinal);
        }

        private static string ToMeta(INamedTypeSymbol symbol)
            => symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
    }
}
