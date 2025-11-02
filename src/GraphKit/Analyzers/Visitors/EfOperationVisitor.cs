using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed class EfOperationVisitor
    {
        private readonly ProjectAnalyzer _analyzer;
        private readonly string _assembly;
        private readonly string _project;
        private readonly Action<string?, string, string, int> _recordAccess;

        public EfOperationVisitor(
            ProjectAnalyzer analyzer,
            string assembly,
            string project,
            Action<string?, string, string, int> recordAccess)
        {
            _analyzer = analyzer;
            _assembly = assembly;
            _project = project;
            _recordAccess = recordAccess;
        }

        public bool TryProcess(IInvocationOperation invocation)
        {
            if (!IsEntityFrameworkInvocation(invocation))
            {
                return false;
            }

            var entitySymbol = GetEntityType(invocation);
            if (entitySymbol is null)
            {
                return false;
            }

            var entityDisplay = entitySymbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            var qualifiedEntity = _analyzer.QualifyTypeName(entityDisplay, _assembly, _project) ?? entityDisplay;
            var entitySimple = GetTopLevelSimpleIdentifier(qualifiedEntity);
            if (string.IsNullOrWhiteSpace(entitySimple))
            {
                return false;
            }

            var contextSymbol = GetContextSymbol(invocation);
            string? contextName = null;
            if (contextSymbol is not null)
            {
                var contextDisplay = contextSymbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                contextName = _analyzer.QualifyTypeName(contextDisplay, _assembly, _project) ?? contextDisplay;
            }

            var operation = DetermineEfOperation(invocation.TargetMethod.Name);
            var line = GetInvocationLine(invocation);

            _recordAccess(contextName, entitySimple, operation, line);
            return true;
        }

        private static bool IsEntityFrameworkInvocation(IInvocationOperation invocation)
        {
            var containing = invocation.TargetMethod.ContainingType;
            if (containing is null)
            {
                return false;
            }

            var display = containing.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (display.Contains("EntityFrameworkCore", StringComparison.Ordinal))
            {
                return true;
            }

            if (display.Contains("DbSet", StringComparison.Ordinal))
            {
                return true;
            }

            if (display.Contains("DbContext", StringComparison.Ordinal))
            {
                return true;
            }

            return invocation.Instance?.Type is INamedTypeSymbol instanceNamed &&
                   (instanceNamed.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat).Contains("EntityFrameworkCore", StringComparison.Ordinal) ||
                    instanceNamed.Name.Contains("DbSet", StringComparison.OrdinalIgnoreCase));
        }

        private static ITypeSymbol? GetEntityType(IInvocationOperation invocation)
        {
            if (invocation.TargetMethod.IsGenericMethod &&
                invocation.TargetMethod.TypeArguments.Length == 1 &&
                string.Equals(invocation.TargetMethod.Name, "Set", StringComparison.OrdinalIgnoreCase))
            {
                return invocation.TargetMethod.TypeArguments[0];
            }

            if (TryExtractEntity(invocation.Instance?.Type, out var entity))
            {
                return entity;
            }

            if (invocation.TargetMethod.IsExtensionMethod && invocation.Arguments.Length > 0)
            {
                if (TryExtractEntity(invocation.Arguments[0].Value.Type, out entity))
                {
                    return entity;
                }
            }

            if (TryExtractEntity(invocation.TargetMethod.ReturnType, out entity))
            {
                return entity;
            }

            return null;
        }

        private static bool TryExtractEntity(ITypeSymbol? symbol, out ITypeSymbol entity)
        {
            entity = default!;
            if (symbol is not INamedTypeSymbol named)
            {
                return false;
            }

            if (named.IsGenericType)
            {
                var constructed = named.ConstructedFrom?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat) ?? string.Empty;
                if ((string.Equals(named.Name, "DbSet", StringComparison.OrdinalIgnoreCase) ||
                     constructed.Contains("EntityFrameworkCore.DbSet", StringComparison.Ordinal)) &&
                    named.TypeArguments.Length == 1)
                {
                    entity = named.TypeArguments[0];
                    return true;
                }

                if ((string.Equals(named.Name, "IQueryable", StringComparison.OrdinalIgnoreCase) ||
                     constructed.StartsWith("System.Linq.IQueryable", StringComparison.Ordinal)) &&
                    named.TypeArguments.Length == 1)
                {
                    entity = named.TypeArguments[0];
                    return true;
                }

                if ((string.Equals(named.Name, "Task", StringComparison.OrdinalIgnoreCase) ||
                     constructed.StartsWith("System.Threading.Tasks.Task", StringComparison.Ordinal)) &&
                    named.TypeArguments.Length == 1)
                {
                    if (TryExtractEntity(named.TypeArguments[0], out entity))
                    {
                        return true;
                    }
                }

                if (named.TypeArguments.Length == 1 &&
                    (constructed.Contains("IAsyncEnumerable", StringComparison.Ordinal) ||
                     constructed.Contains("IAsyncQueryable", StringComparison.Ordinal)))
                {
                    if (TryExtractEntity(named.TypeArguments[0], out entity))
                    {
                        return true;
                    }
                }
            }

            foreach (var iface in named.AllInterfaces)
            {
                if (TryExtractEntity(iface, out entity))
                {
                    return true;
                }
            }

            if (named.BaseType is not null &&
                TryExtractEntity(named.BaseType, out entity))
            {
                return true;
            }

            return false;
        }

        private static ITypeSymbol? GetContextSymbol(IInvocationOperation invocation)
        {
            if (invocation.Instance?.Type is not null &&
                invocation.Instance.Type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat).Contains("DbContext", StringComparison.Ordinal))
            {
                return invocation.Instance.Type;
            }

            if (invocation.TargetMethod.ContainingType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat).Contains("DbContext", StringComparison.Ordinal))
            {
                return invocation.TargetMethod.ContainingType;
            }

            return null;
        }

        private static string DetermineEfOperation(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return "query";
            }

            if (methodName.StartsWith("Add", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("Create", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("Insert", StringComparison.OrdinalIgnoreCase))
            {
                return "insert";
            }

            if (methodName.StartsWith("Update", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("Attach", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("Save", StringComparison.OrdinalIgnoreCase))
            {
                return "update";
            }

            if (methodName.StartsWith("Remove", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
            {
                return "delete";
            }

            return "query";
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
