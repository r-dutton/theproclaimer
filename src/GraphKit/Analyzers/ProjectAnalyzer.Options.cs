using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void RegisterOptions(
        ProjectInfo project,
        SyntaxTree tree,
        TypeDeclarationSyntax declaration,
        string namespaceName)
    {
        var typeName = declaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? typeName : $"{namespaceName}.{typeName}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, declaration);
        var sectionName = ResolveOptionsSectionName(declaration, typeName);

        var info = new OptionsInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, typeName, sectionName);

        foreach (var property in declaration.Members.OfType<PropertyDeclarationSyntax>())
        {
            if (property.Initializer?.Value is LiteralExpressionSyntax literal &&
                literal.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralExpression))
            {
                info.Values[property.Identifier.Text] = literal.Token.ValueText;
            }
            else if (property.Initializer?.Value is LiteralExpressionSyntax numberLiteral &&
                     numberLiteral.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.NumericLiteralExpression))
            {
                info.Values[property.Identifier.Text] = numberLiteral.Token.ValueText;
            }
        }

        _options[fqdn] = info;
        PopulateOptionsFromConfiguration(info);
    }

    private static string? ResolveOptionsSectionName(TypeDeclarationSyntax declaration, string typeName)
    {
        foreach (var field in declaration.Members.OfType<FieldDeclarationSyntax>())
        {
            if (!field.Modifiers.Any(m => m.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.ConstKeyword)))
            {
                continue;
            }

            if (field.Declaration.Type.ToString().Equals("string", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var variable in field.Declaration.Variables)
                {
                    if (variable.Identifier.Text.Equals("SectionName", StringComparison.OrdinalIgnoreCase) &&
                        variable.Initializer?.Value is LiteralExpressionSyntax literal &&
                        literal.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralExpression))
                    {
                        return literal.Token.ValueText;
                    }
                }
            }
        }

        if (typeName.EndsWith("Options", StringComparison.Ordinal))
        {
            return typeName.Replace("Options", string.Empty, StringComparison.Ordinal);
        }

        return null;
    }

    private void UpdateOptionsFromConfiguration()
    {
        foreach (var options in _options.Values)
        {
            PopulateOptionsFromConfiguration(options);
        }
    }

    private void PopulateOptionsFromConfiguration(OptionsInfo options)
    {
        var candidates = new List<KeyValuePair<string, ConfigurationValue>>();
        if (!string.IsNullOrWhiteSpace(options.SectionName))
        {
            var prefix = options.SectionName + ":";
            candidates.AddRange(_configurationValues
                .Where(pair => pair.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .Select(pair => KeyValuePair.Create(pair.Key.Substring(prefix.Length), pair.Value)));
        }

        var fallbackPrefix = options.Name + ":";
        candidates.AddRange(_configurationValues
            .Where(pair => pair.Key.StartsWith(fallbackPrefix, StringComparison.OrdinalIgnoreCase))
            .Select(pair => KeyValuePair.Create(pair.Key.Substring(fallbackPrefix.Length), pair.Value)));

        foreach (var kvp in candidates)
        {
            if (string.IsNullOrWhiteSpace(kvp.Key))
            {
                continue;
            }

            options.Values[kvp.Key] = kvp.Value.Value;
        }
    }

    private OptionsInfo? FindOptionsByType(string optionsType)
    {
        if (_options.TryGetValue(optionsType, out var info))
        {
            return info;
        }

        var simple = optionsType.Split('.').Last();
        return _options.Values.FirstOrDefault(o =>
            o.Fqdn.Equals(optionsType, StringComparison.OrdinalIgnoreCase) ||
            o.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));
    }

    private static string? TryResolveOptionsType(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName) || !typeName.Contains('<', StringComparison.Ordinal))
        {
            return null;
        }

        var normalized = typeName.Replace(" ", string.Empty, StringComparison.Ordinal);
        string[] interfaces = { "IOptions", "IOptionsSnapshot", "IOptionsMonitor" };
        foreach (var iface in interfaces)
        {
            var token = iface + "<";
            var index = normalized.IndexOf(token, StringComparison.Ordinal);
            if (index < 0)
            {
                continue;
            }

            var start = normalized.IndexOf('<', index);
            var end = normalized.LastIndexOf('>');
            if (start >= 0 && end > start)
            {
                return normalized.Substring(start + 1, end - start - 1);
            }
        }

        return null;
    }

    private string? EnsureOptionsNode(string optionsType)
    {
        var info = FindOptionsByType(optionsType);
        if (info is null)
        {
            return null;
        }

        var id = StableId.For("config.options", info.Fqdn, info.Assembly, info.SymbolId);
        if (_nodes.ContainsKey(id))
        {
            return id;
        }

        var props = new Dictionary<string, object>
        {
            ["section"] = info.SectionName ?? info.Name
        };

        if (info.Values.Count > 0)
        {
            props["values"] = info.Values
                .OrderBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(pair => pair.Key, pair => (object)pair.Value, StringComparer.OrdinalIgnoreCase);
        }

        _nodes[id] = new GraphNode
        {
            Id = id,
            Type = "config.options",
            Name = info.Name,
            Fqdn = info.Fqdn,
            Assembly = info.Assembly,
            Project = info.Project,
            FilePath = info.FilePath,
            Span = info.Span,
            SymbolId = info.SymbolId,
            Tags = new[] { "configuration" },
            Props = props
        };

        return id;
    }

    private void EmitOptions()
    {
        foreach (var _ in _options)
        {
            // Nodes are materialized lazily via EnsureOptionsNode when referenced.
            // This loop exists to make sure configuration values stay up to date.
        }
    }
}
