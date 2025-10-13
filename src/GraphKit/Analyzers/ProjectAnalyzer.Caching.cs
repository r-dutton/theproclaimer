using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private static bool IsCacheService(string typeName)
        => typeName.Contains("IMemoryCache", StringComparison.Ordinal)
            || typeName.Contains("IDistributedCache", StringComparison.Ordinal)
            || typeName.Contains("IHybridCache", StringComparison.Ordinal);

    private CacheInvocation? TryCaptureCacheInvocation(
        MemberAccessExpressionSyntax memberAccess,
        InvocationExpressionSyntax? invocation,
        string cacheType,
        SyntaxTree tree)
    {
        invocation ??= memberAccess.Parent as InvocationExpressionSyntax;
        if (invocation is null)
        {
            return null;
        }

        var methodName = memberAccess.Name switch
        {
            IdentifierNameSyntax identifier => identifier.Identifier.Text,
            GenericNameSyntax generic => generic.Identifier.Text,
            _ => null
        };

        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        var key = ExtractCacheKey(invocation);
        var line = GetLineNumber(tree, invocation);
        var operation = DetermineCacheOperation(methodName!);
        RegisterCache(cacheType);
        return new CacheInvocation(cacheType, methodName!, key, line, operation);
    }

    private void RegisterCache(string cacheType)
    {
        _caches.GetOrAdd(cacheType, static type => new CacheInfo(type, DetermineCacheKind(type)));
    }

    private string EnsureCacheNode(string cacheType)
    {
        var info = _caches.GetOrAdd(cacheType, static type => new CacheInfo(type, DetermineCacheKind(type)));
        var nodeType = info.Kind switch
        {
            "memory" => "cache.memory",
            "distributed" => "cache.distributed",
            _ => "cache.component"
        };

        var id = StableId.For(nodeType, info.TypeName, "external", info.TypeName);
        if (!_nodes.ContainsKey(id))
        {
            var name = info.TypeName.Split('.').Last();
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = nodeType,
                Name = name,
                Fqdn = info.TypeName,
                Assembly = "external",
                Project = string.Empty,
                FilePath = string.Empty,
                SymbolId = info.TypeName,
                Tags = new[] { "cache" }
            };
        }

        return id;
    }

    private static string DetermineCacheKind(string cacheType)
    {
        if (cacheType.Contains("Memory", StringComparison.OrdinalIgnoreCase))
        {
            return "memory";
        }

        if (cacheType.Contains("Distributed", StringComparison.OrdinalIgnoreCase))
        {
            return "distributed";
        }

        return "component";
    }

    private static string DetermineCacheOperation(string methodName)
    {
        if (methodName.StartsWith("Get", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("TryGet", StringComparison.OrdinalIgnoreCase))
        {
            return "read";
        }

        if (methodName.StartsWith("Set", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Create", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Remove", StringComparison.OrdinalIgnoreCase))
        {
            return "write";
        }

        if (methodName.Contains("OrCreate", StringComparison.OrdinalIgnoreCase))
        {
            return "read_write";
        }

        return "access";
    }

    private static string? ExtractCacheKey(InvocationExpressionSyntax invocation)
    {
        if (invocation.ArgumentList.Arguments.Count == 0)
        {
            return null;
        }

        var first = invocation.ArgumentList.Arguments[0].Expression;
        return first switch
        {
            LiteralExpressionSyntax literal when literal.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralExpression)
                => literal.Token.ValueText,
            InterpolatedStringExpressionSyntax interpolated
                => string.Concat(interpolated.Contents.Select(content => content switch
                {
                    InterpolatedStringTextSyntax text => text.TextToken.ValueText,
                    _ => "{*}"
                })),
            InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.Text: "nameof" }, ArgumentList.Arguments: { Count: > 0 } args }
                => args[0].Expression switch
                {
                    IdentifierNameSyntax identifier => identifier.Identifier.Text,
                    _ => null
                },
            _ => null
        };
    }
}
