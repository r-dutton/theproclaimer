using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Ef
{
    public static class EfRecognizer
    {
        public static bool IsDbContext(ITypeSymbol t) => t.Name.EndsWith("DbContext");
        public static bool IsSaveChanges(IMethodSymbol m) => m.Name is "SaveChanges" or "SaveChangesAsync";
        public static bool IsWrite(IMethodSymbol m) => m.Name is "Add" or "AddAsync" or "Update" or "Remove";

        public static ITypeSymbol? TryGetDbSetEntity(IOperation op)
        {
            switch (op)
            {
                case IPropertyReferenceOperation prop when prop.Property.Type is INamedTypeSymbol nts && nts.Name == "DbSet" && nts.TypeArguments.Length == 1:
                    return nts.TypeArguments[0];
                case IInvocationOperation inv when inv.TargetMethod.Name == "Set" && inv.TargetMethod.TypeArguments.Length == 1:
                    return inv.TargetMethod.TypeArguments[0];
            }
            return null;
        }
    }
}
