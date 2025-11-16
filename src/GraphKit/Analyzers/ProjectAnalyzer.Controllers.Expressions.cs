using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private string RenderInterpolatedString(InterpolatedStringExpressionSyntax interpolated)
    {
        var builder = new StringBuilder();
        foreach (var content in interpolated.Contents)
        {
            switch (content)
            {
                case InterpolatedStringTextSyntax text:
                    builder.Append(text.TextToken.ValueText);
                    break;
                case InterpolationSyntax interpolation:
                    var evaluated = TryEvaluateStringConstant(interpolation.Expression);
                    if (!string.IsNullOrWhiteSpace(evaluated))
                    {
                        builder.Append(evaluated);
                    }
                    else
                    {
                        builder.Append("{*}");
                    }
                    break;
                default:
                    builder.Append("{*}");
                    break;
            }
        }
        return builder.ToString();
    }

    private string? ExtractStringValue(ExpressionSyntax expression)
    {
        if (TryEvaluateStringConstant(expression) is { } evaluatedConstant)
        {
            return evaluatedConstant;
        }

        switch (expression)
        {
            case LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.StringLiteralExpression):
                return literal.Token.ValueText;
            case LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.NullLiteralExpression):
                return null;
            case InterpolatedStringExpressionSyntax interpolated:
                return RenderInterpolatedString(interpolated);
            case InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.Text: "nameof" } } nameofInvocation:
                return nameofInvocation.ArgumentList?.Arguments.FirstOrDefault()?.Expression.ToString();
            default:
                return expression.ToString().Trim('"');
        }
    }

    private string? ResolveStringValue(ExpressionSyntax? expression)
    {
        if (expression is null)
        {
            return null;
        }

        if (TryEvaluateStringConstant(expression) is { } evaluatedConstant)
        {
            return evaluatedConstant;
        }

        if (expression is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.StringLiteralExpression))
        {
            return literal.Token.ValueText;
        }

        if (expression is InterpolatedStringExpressionSyntax interpolated)
        {
            return RenderInterpolatedString(interpolated);
        }

        var expressionText = expression.ToString();
        if (_stringConstants.TryGetValue(expressionText, out var value))
        {
            return value;
        }

        return expression switch
        {
            IdentifierNameSyntax identifier when _stringConstants.TryGetValue(identifier.Identifier.Text, out var identifierValue) => identifierValue,
            MemberAccessExpressionSyntax memberAccess when _stringConstants.TryGetValue(memberAccess.Name.Identifier.Text, out var memberValue) => memberValue,
            _ => null
        };
    }

    private string? ResolveRouteFromExpression(ExpressionSyntax? expression, IReadOnlyDictionary<string, string>? localValues = null)
    {
        if (expression is null)
        {
            return null;
        }

        if (expression is IdentifierNameSyntax identifier)
        {
            if (localValues is not null && localValues.TryGetValue(identifier.Identifier.Text, out var localValue))
            {
                return NormalizeRoute(localValue);
            }

            if (_stringConstants.TryGetValue(identifier.Identifier.Text, out var identifierValue))
            {
                return NormalizeRoute(identifierValue);
            }
        }

        // Handle wrappers like QueryHelpers.AddQueryString(url, ...)
        if (expression is InvocationExpressionSyntax invoked)
        {
            // Try match AddQueryString on QueryHelpers or any AddQueryString(...) style
            static bool IsAddQueryString(SyntaxNode node)
            {
                return node switch
                {
                    IdentifierNameSyntax ident => string.Equals(ident.Identifier.Text, "AddQueryString", StringComparison.Ordinal),
                    GenericNameSyntax g => string.Equals(g.Identifier.Text, "AddQueryString", StringComparison.Ordinal),
                    MemberAccessExpressionSyntax member => IsAddQueryString(member.Name),
                    _ => false
                };
            }

            if (IsAddQueryString(invoked.Expression))
            {
                // Base URL is the first argument; ignore query additions for route matching
                var firstArg = invoked.ArgumentList?.Arguments.FirstOrDefault()?.Expression;
                var baseRoute = ExtractRouteLiteral(invoked.SyntaxTree, firstArg) ?? ResolveRouteFromExpression(firstArg, localValues);
                if (!string.IsNullOrWhiteSpace(baseRoute))
                {
                    return NormalizeRoute(baseRoute!);
                }
            }
        }

        var expressionText = expression.ToString();
        if (_stringConstants.TryGetValue(expressionText, out var constantValue))
        {
            return NormalizeRoute(constantValue);
        }

        if (expression is MemberAccessExpressionSyntax memberAccess)
        {
            if (_stringConstants.TryGetValue(memberAccess.Name.Identifier.Text, out var memberValue))
            {
                return NormalizeRoute(memberValue);
            }

            var memberAccessText = memberAccess.ToString();
            if (_stringConstants.TryGetValue(memberAccessText, out var fullValue))
            {
                return NormalizeRoute(fullValue);
            }
        }

        if (expression is InterpolatedStringExpressionSyntax interpolated)
        {
            var rendered = RenderInterpolatedString(interpolated);
            return NormalizeRoute(rendered);
        }

        return null;
    }

    private string? ResolveRoute(SyntaxList<AttributeListSyntax> attributes, string className)
    {
        foreach (var attribute in attributes.SelectMany(list => list.Attributes))
        {
            var name = attribute.Name.ToString();
            if (!name.Contains("Route", StringComparison.Ordinal) && !name.StartsWith("Http", StringComparison.Ordinal))
            {
                continue;
            }

            if (attribute.ArgumentList is not { Arguments.Count: > 0 } argumentList)
            {
                continue;
            }

            foreach (var argument in argumentList.Arguments)
            {
                if (argument is null)
                {
                    continue;
                }

                if (argument.NameEquals is { Name: IdentifierNameSyntax equalsName } &&
                    !equalsName.Identifier.Text.Equals("Template", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (argument.NameColon is { Name.Identifier.Text: var colonName } &&
                    !colonName.Equals("template", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var template = ResolveStringValue(argument.Expression);
                if (string.IsNullOrWhiteSpace(template))
                {
                    continue;
                }

                var resolvedController = className.Replace("Controller", string.Empty, StringComparison.OrdinalIgnoreCase);
                var normalized = template.Replace("[controller]", resolvedController, StringComparison.OrdinalIgnoreCase);
                return NormalizeRoute(normalized);
            }
        }

        return null;
    }

    private static string NormalizeRoute(string route)
    {
        if (string.IsNullOrWhiteSpace(route))
        {
            return "/";
        }

        if (route.Contains("://", StringComparison.Ordinal))
        {
            return route;
        }

        return route.StartsWith("/", StringComparison.Ordinal) ? route : "/" + route;
    }

    private static string CanonicalizeRoute(string route)
    {
        var normalized = NormalizeRoute(route);
        return Regex.Replace(normalized, "\\{[^}]+\\}", "{*}");
    }

    private string? ExtractRouteLiteral(SyntaxTree tree, ExpressionSyntax? expression)
    {
        if (expression is null)
        {
            return null;
        }

        if (TryEvaluateStringConstant(expression) is { } evaluated)
        {
            return NormalizeRoute(evaluated);
        }

        switch (expression)
        {
            case LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.StringLiteralExpression):
                return NormalizeRoute(literal.Token.ValueText);
            case InterpolatedStringExpressionSyntax interpolated:
                var text = RenderInterpolatedString(interpolated);
                return NormalizeRoute(text);
            default:
                return null;
        }
    }
}
