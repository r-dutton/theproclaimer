using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private static bool IsConfigurationType(string? typeName)
        => !string.IsNullOrWhiteSpace(typeName)
            && typeName!.Contains("IConfiguration", StringComparison.Ordinal);

    private ConfigurationUsage? TryCaptureConfigurationUsage(
        MemberAccessExpressionSyntax memberAccess,
        InvocationExpressionSyntax invocation,
        string configurationType,
        SyntaxTree tree)
    {
        var methodName = GetMemberName(memberAccess.Name);
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
        var key = argument is null ? null : ExtractConfigurationKey(argument);
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        var line = GetLineNumber(tree, invocation);
        var filePath = GetRelativePath(tree.FilePath);
        return new ConfigurationUsage(configurationType, methodName!, key, line, filePath);
    }

    private ConfigurationUsage? TryCaptureConfigurationIndexer(
        ElementAccessExpressionSyntax elementAccess,
        string configurationType,
        SyntaxTree tree)
    {
        var argument = elementAccess.ArgumentList.Arguments.FirstOrDefault()?.Expression;
        var key = argument is null ? null : ExtractConfigurationKey(argument);
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        var line = GetLineNumber(tree, elementAccess);
        var filePath = GetRelativePath(tree.FilePath);
        return new ConfigurationUsage(configurationType, "indexer", key, line, filePath);
    }

    private static string? ExtractConfigurationKey(ExpressionSyntax expression)
    {
        return expression switch
        {
            LiteralExpressionSyntax literal when literal.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralExpression)
                => literal.Token.ValueText,
            InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.Text: "nameof" }, ArgumentList.Arguments.Count: > 0 } nameofInvocation
                => nameofInvocation.ArgumentList.Arguments[0].Expression switch
                {
                    IdentifierNameSyntax identifier => identifier.Identifier.Text,
                    _ => null
                },
            _ => null
        };
    }

    private string EnsureConfigurationNode(string key)
    {
        var id = StableId.For("config.value", key, "configuration", key);
        if (_nodes.ContainsKey(id))
        {
            return id;
        }

        if (_configurationValues.TryGetValue(key, out var configuration))
        {
            var props = new Dictionary<string, object>
            {
                ["value"] = configuration.Value
            };

            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "config.value",
                Name = key,
                Fqdn = key,
                Assembly = "configuration",
                Project = string.Empty,
                FilePath = configuration.FilePath,
                Span = configuration.Span,
                SymbolId = key,
                Tags = new[] { "configuration" },
                Props = props
            };
        }
        else
        {
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "config.value",
                Name = key,
                Fqdn = key,
                Assembly = "configuration",
                Project = string.Empty,
                FilePath = string.Empty,
                Span = null,
                SymbolId = key,
                Tags = new[] { "configuration" }
            };
        }

        return id;
    }

    private void EmitConfigurationEdges(string ownerId, IEnumerable<ConfigurationUsage> usages)
    {
        var comparer = new ConfigurationUsageComparer();
        var seen = new HashSet<(string Key, string Accessor, string FilePath)>(comparer);

        foreach (var usage in usages
            .Where(u => !string.IsNullOrWhiteSpace(u.Key))
            .OrderBy(u => u.Line))
        {
            var accessor = usage.Accessor ?? string.Empty;
            var signature = (usage.Key!, accessor, usage.FilePath);
            if (!seen.Add(signature))
            {
                continue;
            }

            var configId = EnsureConfigurationNode(usage.Key!);
            var props = new Dictionary<string, object>
            {
                ["key"] = usage.Key!,
                ["accessor"] = accessor
            };

            if (_configurationValues.TryGetValue(usage.Key!, out var configuration))
            {
                props["value"] = configuration.Value;
                props["source_file"] = configuration.FilePath;
            }

            _edges.Add(new GraphEdge
            {
                From = ownerId,
                To = configId,
                Kind = "uses_configuration",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "configuration.access",
                    Location = new GraphLocation { File = usage.FilePath, Line = usage.Line }
                },
                Props = props,
                Evidence = CreateEvidence(usage.FilePath, usage.Line)
            });
        }
    }

    private sealed class ConfigurationUsageComparer : IEqualityComparer<(string Key, string Accessor, string FilePath)>
    {
        public bool Equals((string Key, string Accessor, string FilePath) x, (string Key, string Accessor, string FilePath) y)
            => string.Equals(x.Key, y.Key, StringComparison.OrdinalIgnoreCase)
                && string.Equals(x.Accessor, y.Accessor, StringComparison.OrdinalIgnoreCase)
                && string.Equals(x.FilePath, y.FilePath, StringComparison.OrdinalIgnoreCase);

        public int GetHashCode((string Key, string Accessor, string FilePath) obj)
        {
            var keyHash = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Key);
            var accessorHash = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Accessor);
            var fileHash = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.FilePath);
            return HashCode.Combine(keyHash, accessorHash, fileHash);
        }
    }
}
