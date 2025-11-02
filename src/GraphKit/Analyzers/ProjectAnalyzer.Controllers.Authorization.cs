using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private AuthorizationMetadata CollectAuthorizationAttributes(SyntaxTree tree, SyntaxList<AttributeListSyntax> attributeLists, string source)
    {
        var requirements = new List<EndpointAuthorization>();
        var allowsAnonymous = false;

        foreach (var attribute in attributeLists.SelectMany(list => list.Attributes))
        {
            var attributeName = attribute.Name.ToString();
            if (AttributeNameEquals(attributeName, "Authorize"))
            {
                requirements.Add(ParseAuthorizeAttribute(tree, attribute, source));
            }
            else if (AttributeNameEquals(attributeName, "AllowAnonymous"))
            {
                allowsAnonymous = true;
            }
        }

        return new AuthorizationMetadata(requirements, allowsAnonymous);
    }

    private void ApplyMinimalEndpointAuthorization(SyntaxTree tree, InvocationExpressionSyntax mapInvocation, MinimalEndpointInfo endpoint)
    {
        SyntaxNode? current = mapInvocation;
        while (current.Parent is MemberAccessExpressionSyntax memberAccess &&
               memberAccess.Parent is InvocationExpressionSyntax parentInvocation)
        {
            var methodName = memberAccess.Name.Identifier.Text;
            var metadata = CollectAuthorizationFromEndpointInvocation(tree, parentInvocation, methodName);

            if (metadata.AllowsAnonymous)
            {
                endpoint.Authorizations.Clear();
                endpoint.AllowsAnonymous = true;
            }

            if (metadata.Requirements.Count > 0)
            {
                endpoint.AllowsAnonymous = false;
                endpoint.Authorizations.AddRange(metadata.Requirements);
            }

            current = parentInvocation;
        }
    }

    private AuthorizationMetadata CollectAuthorizationFromEndpointInvocation(SyntaxTree tree, InvocationExpressionSyntax invocation, string source)
    {
        var requirements = new List<EndpointAuthorization>();
        var allowsAnonymous = false;

        if (invocation.Expression is not MemberAccessExpressionSyntax memberAccess)
        {
            return new AuthorizationMetadata(requirements, allowsAnonymous);
        }

        var methodName = memberAccess.Name.Identifier.Text;
        if (methodName.Equals("RequireAuthorization", StringComparison.OrdinalIgnoreCase))
        {
            CollectAuthorizationFromRequireAuthorization(tree, invocation, $"require_authorization:{source}", requirements);
        }
        else if (methodName.Equals("AllowAnonymous", StringComparison.OrdinalIgnoreCase))
        {
            allowsAnonymous = true;
        }
        else if (methodName.Equals("WithMetadata", StringComparison.OrdinalIgnoreCase))
        {
            CollectAuthorizationFromMetadata(tree, invocation, $"metadata:{source}", requirements, ref allowsAnonymous);
        }

        return new AuthorizationMetadata(requirements, allowsAnonymous);
    }

    private void CollectAuthorizationFromRequireAuthorization(SyntaxTree tree, InvocationExpressionSyntax invocation, string source, List<EndpointAuthorization> requirements)
    {
        var line = GetLineNumber(tree, invocation);
        if (invocation.ArgumentList is null || invocation.ArgumentList.Arguments.Count == 0)
        {
            requirements.Add(new EndpointAuthorization(null, null, null, source, line));
            return;
        }

        foreach (var argument in invocation.ArgumentList.Arguments)
        {
            var allowAnonymous = false;
            if (TryExtractAuthorizationMetadata(tree, argument.Expression, source, requirements, ref allowAnonymous))
            {
                continue;
            }

            var value = ExtractStringValue(argument.Expression);
            if (!string.IsNullOrWhiteSpace(value))
            {
                var argumentLine = GetLineNumber(tree, argument.Expression);
                requirements.Add(new EndpointAuthorization(value, null, null, source, argumentLine));
            }
            else
            {
                requirements.Add(new EndpointAuthorization(null, null, null, source, line));
            }
        }
    }

    private void CollectAuthorizationFromMetadata(SyntaxTree tree, InvocationExpressionSyntax invocation, string source, List<EndpointAuthorization> requirements, ref bool allowsAnonymous)
    {
        if (invocation.ArgumentList is null)
        {
            return;
        }

        foreach (var argument in invocation.ArgumentList.Arguments)
        {
            TryExtractAuthorizationMetadata(tree, argument.Expression, source, requirements, ref allowsAnonymous);
        }
    }

    private bool TryExtractAuthorizationMetadata(
        SyntaxTree tree,
        ExpressionSyntax expression,
        string source,
        List<EndpointAuthorization> requirements,
        ref bool allowsAnonymous)
    {
        switch (expression)
        {
            case ObjectCreationExpressionSyntax creation:
                var typeName = creation.Type.ToString();
                if (typeName.EndsWith("AuthorizeAttribute", StringComparison.Ordinal))
                {
                    requirements.Add(ParseAuthorizeObjectCreation(tree, creation, source));
                    return true;
                }

                if (typeName.EndsWith("AllowAnonymousAttribute", StringComparison.Ordinal))
                {
                    allowsAnonymous = true;
                    return true;
                }

                break;
            case ArrayCreationExpressionSyntax array:
                if (array.Initializer is null)
                {
                    return false;
                }

                var handledAny = false;
                foreach (var element in array.Initializer.Expressions)
                {
                    if (TryExtractAuthorizationMetadata(tree, element, source, requirements, ref allowsAnonymous))
                    {
                        handledAny = true;
                    }
                    else
                    {
                        var value = ExtractStringValue(element);
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            var elementLine = GetLineNumber(tree, element);
                            requirements.Add(new EndpointAuthorization(value, null, null, source, elementLine));
                            handledAny = true;
                        }
                    }
                }

                return handledAny;
            case ImplicitArrayCreationExpressionSyntax implicitArray:
                var handled = false;
                foreach (var element in implicitArray.Initializer.Expressions)
                {
                    if (TryExtractAuthorizationMetadata(tree, element, source, requirements, ref allowsAnonymous))
                    {
                        handled = true;
                    }
                    else
                    {
                        var value = ExtractStringValue(element);
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            var elementLine = GetLineNumber(tree, element);
                            requirements.Add(new EndpointAuthorization(value, null, null, source, elementLine));
                            handled = true;
                        }
                    }
                }

                return handled;
        }

        return false;
    }

    private EndpointAuthorization ParseAuthorizeAttribute(SyntaxTree tree, AttributeSyntax attribute, string source)
    {
        string? policy = null;
        string? roles = null;
        string? authenticationSchemes = null;

        if (attribute.ArgumentList is not null)
        {
            foreach (var argument in attribute.ArgumentList.Arguments)
            {
                var value = ExtractStringValue(argument.Expression);
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                var argumentName = GetArgumentName(argument);
                if (string.IsNullOrWhiteSpace(argumentName))
                {
                    policy ??= value;
                }
                else
                {
                    switch (argumentName)
                    {
                        case "Policy":
                            policy = value;
                            break;
                        case "Roles":
                            roles = value;
                            break;
                        case "AuthenticationSchemes":
                            authenticationSchemes = value;
                            break;
                    }
                }
            }
        }

        var line = GetLineNumber(tree, attribute);
        return new EndpointAuthorization(policy, roles, authenticationSchemes, source, line);
    }

    private EndpointAuthorization ParseAuthorizeObjectCreation(SyntaxTree tree, ObjectCreationExpressionSyntax creation, string source)
    {
        string? policy = null;
        string? roles = null;
        string? authenticationSchemes = null;

        if (creation.ArgumentList is not null)
        {
            foreach (var argument in creation.ArgumentList.Arguments)
            {
                var value = ExtractStringValue(argument.Expression);
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                var argumentName = GetArgumentName(argument);
                if (string.IsNullOrWhiteSpace(argumentName))
                {
                    policy ??= value;
                }
                else
                {
                    switch (argumentName)
                    {
                        case "policy":
                        case "Policy":
                            policy = value;
                            break;
                        case "roles":
                        case "Roles":
                            roles = value;
                            break;
                        case "authenticationSchemes":
                        case "AuthenticationSchemes":
                            authenticationSchemes = value;
                            break;
                    }
                }
            }
        }

        if (creation.Initializer is not null)
        {
            foreach (var assignment in creation.Initializer.Expressions.OfType<AssignmentExpressionSyntax>())
            {
                if (assignment.Left is IdentifierNameSyntax { Identifier.Text: var propertyName })
                {
                    var value = ExtractStringValue(assignment.Right);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    switch (propertyName)
                    {
                        case "Policy":
                            policy = value;
                            break;
                        case "Roles":
                            roles = value;
                            break;
                        case "AuthenticationSchemes":
                            authenticationSchemes = value;
                            break;
                    }
                }
            }
        }

        var line = GetLineNumber(tree, creation);
        return new EndpointAuthorization(policy, roles, authenticationSchemes, source, line);
    }

    private static string? GetArgumentName(ArgumentSyntax argument)
        => argument.NameColon?.Name.Identifier.Text;

    private static string? GetArgumentName(AttributeArgumentSyntax argument)
        => argument.NameEquals?.Name.Identifier.Text ?? argument.NameColon?.Name.Identifier.Text;

    private static bool AttributeNameEquals(string attributeName, string expected)
    {
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            return false;
        }

        var simpleName = GetTopLevelSimpleIdentifier(attributeName);
        if (simpleName.EndsWith("Attribute", StringComparison.OrdinalIgnoreCase))
        {
            simpleName = simpleName[..^9];
        }

        return simpleName.Equals(expected, StringComparison.OrdinalIgnoreCase);
    }

    private static Dictionary<string, object> CreateAuthorizationProps(EndpointAuthorization authorization)
    {
        var props = new Dictionary<string, object>
        {
            ["source"] = authorization.Source
        };

        if (!string.IsNullOrWhiteSpace(authorization.Policy))
        {
            props["policy"] = authorization.Policy!;
        }

        if (!string.IsNullOrWhiteSpace(authorization.Roles))
        {
            props["roles"] = authorization.Roles!;
        }

        if (!string.IsNullOrWhiteSpace(authorization.AuthenticationSchemes))
        {
            props["authentication_schemes"] = authorization.AuthenticationSchemes!;
        }

        if (authorization.Line > 0)
        {
            props["line"] = authorization.Line;
        }

        return props;
    }
}
