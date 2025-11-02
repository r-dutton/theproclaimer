using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void CapturePublisherProxy(
        ProjectInfo project,
        SyntaxTree tree,
        ClassDeclarationSyntax classDeclaration,
        string namespaceName,
        IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var fieldLookup = new Dictionary<string, FieldDescriptor>(StringComparer.OrdinalIgnoreCase);
        foreach (var pair in fieldTypes)
        {
            var normalizedName = pair.Key.TrimStart('_');
            if (normalizedName.Length == 0)
            {
                normalizedName = pair.Key;
            }

            if (!fieldLookup.ContainsKey(normalizedName))
            {
                fieldLookup[normalizedName] = pair.Value;
            }
        }

        var publisherFields = fieldLookup
            .Where(pair => IsLikelyPublisherTypeName(pair.Value.Type))
            .ToDictionary(pair => pair.Key, pair => QualifyTypeName(pair.Value.Type, project.AssemblyName, project.RelativeDirectory), StringComparer.OrdinalIgnoreCase);

        var implementedContracts = classDeclaration.BaseList?.Types
            .Select(type => QualifyTypeName(type.Type.ToString(), project.AssemblyName, project.RelativeDirectory))
            .Where(type => !string.IsNullOrWhiteSpace(type))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList()
            ?? new List<string>();

        var publisherCalls = new List<HandlerPublisherCall>();

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(
                    p => p.Identifier.Text,
                    p => p.Type is null ? null : QualifyTypeName(p.Type.ToString(), project.AssemblyName, project.RelativeDirectory),
                    StringComparer.OrdinalIgnoreCase);

            var localVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var local in Descendants<LocalDeclarationStatementSyntax>(method))
            {
                var declaredType = local.Declaration.Type.ToString();
                foreach (var variable in local.Declaration.Variables)
                {
                    var resolvedType = declaredType;
                    if (string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase) &&
                        variable.Initializer?.Value is ObjectCreationExpressionSyntax creation)
                    {
                        resolvedType = creation.Type.ToString();
                    }

                    resolvedType = QualifyTypeName(resolvedType, project.AssemblyName, project.RelativeDirectory);
                    localVariables[variable.Identifier.Text] = resolvedType;
                }
            }

            foreach (var invocation in Descendants<InvocationExpressionSyntax>(method))
            {
                if (invocation.Expression is not MemberAccessExpressionSyntax memberAccess)
                {
                    continue;
                }

                var methodName = GetMemberName(memberAccess.Name);
                if (string.IsNullOrWhiteSpace(methodName))
                {
                    continue;
                }

                if (!methodName.StartsWith("Publish", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var publisherTypeCandidate = ResolvePublisherExpressionType(memberAccess.Expression, fieldLookup, publisherFields, parameterTypes, localVariables);
                if (string.IsNullOrWhiteSpace(publisherTypeCandidate) || !IsLikelyPublisherTypeName(publisherTypeCandidate))
                {
                    continue;
                }

                var qualifiedPublisherType = QualifyTypeName(publisherTypeCandidate!, project.AssemblyName, project.RelativeDirectory);
                var resolvedPublisherType = ResolveImplementationType(qualifiedPublisherType) ?? qualifiedPublisherType;

                var messageType = ResolvePublishedMessageType(invocation, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory);
                if (!string.IsNullOrWhiteSpace(messageType))
                {
                    messageType = QualifyTypeName(messageType!, project.AssemblyName, project.RelativeDirectory);
                }

                var line = GetLineNumber(tree, invocation);
                var containingMember = method.Identifier.Text;
                publisherCalls.Add(new HandlerPublisherCall(resolvedPublisherType ?? qualifiedPublisherType, methodName!, line, messageType, containingMember));
            }
        }

        if (publisherCalls.Count == 0)
        {
            return;
        }

        var serviceContracts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var contract in implementedContracts)
        {
            serviceContracts.Add(contract);
        }

        // Include the interface variants inferred from class name as potential contracts for matching.
        foreach (var candidate in EnumerateTypeKeys(fqdn))
        {
            serviceContracts.Add(candidate);
        }

        serviceContracts.Add(fqdn);

        var proxyInfo = new PublisherProxyInfo(
            fqdn,
            project.AssemblyName,
            project.RelativeDirectory,
            filePath,
            span,
            symbolId,
            className,
            serviceContracts,
            publisherCalls);

        _publisherProxies[fqdn] = proxyInfo;

        foreach (var contract in serviceContracts)
        {
            var bag = _publisherProxyContracts.GetOrAdd(contract, _ => new ConcurrentBag<string>());
            bag.Add(fqdn);
        }
    }

    private void AnalyzePublisher(ProjectInfo project, Microsoft.CodeAnalysis.SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        string? queueOrTopic = null;
        string? subject = null;

        foreach (var invocation in Descendants<InvocationExpressionSyntax>(classDeclaration))
        {
            if (invocation.Expression is not MemberAccessExpressionSyntax { Name.Identifier.Text: var name })
            {
                continue;
            }

            if ((string.Equals(name, "CreateSender", StringComparison.Ordinal) || string.Equals(name, "CreateTopicSender", StringComparison.Ordinal)) &&
                invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is LiteralExpressionSyntax literal &&
                literal.IsKind(SyntaxKind.StringLiteralExpression))
            {
                queueOrTopic ??= literal.Token.ValueText;
            }
        }

        foreach (var creation in Descendants<ObjectCreationExpressionSyntax>(classDeclaration))
        {
            if (!creation.Type.ToString().Contains("ServiceBusMessage", StringComparison.Ordinal))
            {
                continue;
            }

            if (creation.Initializer is null)
            {
                continue;
            }

            foreach (var assignment in creation.Initializer.Expressions.OfType<AssignmentExpressionSyntax>())
            {
                if (assignment.Left is IdentifierNameSyntax { Identifier.Text: "Subject" } &&
                    assignment.Right is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.StringLiteralExpression))
                {
                    subject ??= literal.Token.ValueText;
                }
            }
        }

        var info = new PublisherInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, fieldTypes)
        {
            QueueOrTopic = queueOrTopic,
            Subject = subject
        };

        _publishers[fqdn] = info;
    }

    private void PropagateServicePublisherCalls()
    {
        foreach (var handler in _handlers.Values)
        {
            var seen = new HashSet<string>(handler.PublisherCalls
                .Select(call => $"{call.PublisherType}|{call.MessageType}|{call.Line}")
                , StringComparer.OrdinalIgnoreCase);

            foreach (var usage in handler.ServiceUsages)
            {
                var candidateTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                void AddCandidate(string? candidate)
                {
                    if (!string.IsNullOrWhiteSpace(candidate))
                    {
                        candidateTypes.Add(candidate!);
                    }
                }

                AddCandidate(usage.ServiceType);
                AddCandidate(usage.TargetType);
                if (!string.IsNullOrWhiteSpace(usage.ServiceType))
                {
                    AddCandidate(ResolveImplementationType(usage.ServiceType!));
                }

                if (!string.IsNullOrWhiteSpace(usage.TargetType))
                {
                    AddCandidate(ResolveImplementationType(usage.TargetType!));
                }

                var expandedCandidates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var candidate in candidateTypes)
                {
                    foreach (var alias in EnumerateTypeKeys(candidate))
                    {
                        expandedCandidates.Add(alias);
                    }
                }

                foreach (var candidate in expandedCandidates)
                {
                    if (!TryGetPublisherProxies(candidate, out var proxies))
                    {
                        continue;
                    }

                    foreach (var proxy in proxies)
                    {
                        foreach (var call in proxy.PublisherCalls)
                        {
                            if (!string.IsNullOrWhiteSpace(call.ContainingMember))
                            {
                                var usageMember = usage.InvocationMethod ?? usage.Method;
                                if (string.IsNullOrWhiteSpace(usageMember) ||
                                    !string.Equals(usageMember, call.ContainingMember, StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }
                            }

                            var publisherType = ResolveImplementationType(call.PublisherType) ?? call.PublisherType;
                            var key = $"{publisherType}|{call.MessageType}|{usage.Line}";
                            if (!seen.Add(key))
                            {
                                continue;
                            }

                            handler.PublisherCalls.Add(new HandlerPublisherCall(publisherType, call.Method, usage.Line, call.MessageType, call.ContainingMember));
                        }
                    }
                }
            }
        }
    }

    private void EmitPublishers()
    {
        var createdContracts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var publisher in _publishers.Values)
        {
            var id = StableId.For("message.publisher", publisher.Fqdn, publisher.Assembly, publisher.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "message.publisher",
                Name = publisher.Name,
                Fqdn = publisher.Fqdn,
                Assembly = publisher.Assembly,
                Project = publisher.Project,
                FilePath = publisher.FilePath,
                Span = publisher.Span,
                SymbolId = publisher.SymbolId,
                Tags = new[] { "messaging" },
                Props = new Dictionary<string, object>
                {
                    ["queue"] = publisher.QueueOrTopic ?? string.Empty,
                    ["subject"] = publisher.Subject ?? string.Empty
                }
            };

            var publisherAliases = new[] { publisher.Fqdn, publisher.Name }
                .SelectMany<string, string>(alias =>
                {
                    if (string.IsNullOrWhiteSpace(alias))
                    {
                        return Array.Empty<string>();
                    }

                    var aliases = new List<string> { alias };
                    var simple = GetTopLevelSimpleIdentifier(alias);
                    if (!string.IsNullOrWhiteSpace(simple))
                    {
                        aliases.Add(simple);
                    }

                    return aliases;
                })
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            foreach (var (handler, call) in _handlers.Values
                .SelectMany(handler => handler.PublisherCalls.Select(call => (handler, call)))
                .Where(tuple => publisherAliases.Any(alias => tuple.call.PublisherType.Contains(alias, StringComparison.OrdinalIgnoreCase))))
            {
                var handlerId = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = handlerId,
                    To = id,
                    Kind = "publishes",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "message.publish",
                        Location = new GraphLocation { File = handler.FilePath, Line = call.Line }
                    },
                    Props = call.MessageType is not null
                        ? new Dictionary<string, object> { ["message_type"] = call.MessageType! }
                        : null,
                    Evidence = CreateEvidence(handler.FilePath, call.Line)
                });

                if (string.IsNullOrWhiteSpace(call.MessageType))
                {
                    continue;
                }

                var contract = ResolveMessageContract(call.MessageType!);
                var contractId = StableId.For("message.contract", contract.Fqdn, contract.Assembly, contract.SymbolId);

                if (createdContracts.Add(contractId))
                {
                    _nodes[contractId] = new GraphNode
                    {
                        Id = contractId,
                        Type = "message.contract",
                        Name = contract.Name,
                        Fqdn = contract.Fqdn,
                        Assembly = contract.Assembly,
                        Project = contract.Project,
                        FilePath = contract.FilePath,
                        Span = contract.Span,
                        SymbolId = contract.SymbolId,
                        Tags = new[] { "messaging" }
                    };
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = contractId,
                    Kind = "produces_event",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "message.publish",
                        Location = new GraphLocation { File = publisher.FilePath, Line = publisher.Span.StartLine }
                    },
                    Evidence = CreateEvidence(publisher.FilePath, publisher.Span)
                });
            }
        }
    }

    private MessageContractInfo ResolveMessageContract(string messageType)
    {
        if (_messageContracts.TryGetValue(messageType, out var contract))
        {
            return contract;
        }

        var simple = GetTopLevelSimpleIdentifier(messageType);
        var existing = _messageContracts.Values.FirstOrDefault(c =>
            c.Fqdn.Equals(messageType, StringComparison.OrdinalIgnoreCase) ||
            c.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));

        if (existing is not null)
        {
            return existing;
        }

        var fqdn = messageType.Contains('.') ? messageType : simple;
        var symbolId = $"T:{fqdn}";
        var name = string.IsNullOrWhiteSpace(simple) ? messageType : simple;
        return new MessageContractInfo(fqdn, string.Empty, string.Empty, string.Empty, new GraphSpan { StartLine = 1, EndLine = 1 }, symbolId, name);
    }

    private bool TryGetPublisherProxies(string serviceType, out List<PublisherProxyInfo> proxies)
    {
        proxies = new List<PublisherProxyInfo>();
        if (string.IsNullOrWhiteSpace(serviceType))
        {
            return false;
        }

        if (_publisherProxies.TryGetValue(serviceType, out var direct))
        {
            proxies.Add(direct);
        }

        if (_publisherProxyContracts.TryGetValue(serviceType, out var contractBag))
        {
            foreach (var proxyKey in contractBag)
            {
                if (_publisherProxies.TryGetValue(proxyKey, out var proxy) && !proxies.Contains(proxy))
                {
                    proxies.Add(proxy);
                }
            }
        }

        return proxies.Count > 0;
    }

    private static bool IsLikelyPublisherTypeName(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var simple = GetTopLevelSimpleIdentifier(typeName);
        return typeName.Contains("Publisher", StringComparison.OrdinalIgnoreCase) ||
               simple.Contains("Publisher", StringComparison.OrdinalIgnoreCase);
    }

    private string? ResolvePublisherExpressionType(
        SyntaxNode expression,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        IReadOnlyDictionary<string, string> publisherFields,
        IReadOnlyDictionary<string, string?> parameterTypes,
        IDictionary<string, string> localVariables)
    {
        if (TryResolveFieldDescriptor(expression, fieldLookup, out var descriptor, out var fieldName))
        {
            if (publisherFields.TryGetValue(fieldName, out var qualifiedFieldType))
            {
                return qualifiedFieldType;
            }

            return descriptor.Type;
        }

        if (expression is IdentifierNameSyntax identifier)
        {
            if (localVariables.TryGetValue(identifier.Identifier.Text, out var localType))
            {
                return localType;
            }

            if (parameterTypes.TryGetValue(identifier.Identifier.Text, out var parameterType))
            {
                return parameterType;
            }
        }

        if (expression is MemberAccessExpressionSyntax access)
        {
            return ResolvePublisherExpressionType(access.Expression, fieldLookup, publisherFields, parameterTypes, localVariables);
        }

        return null;
    }

    private string? ResolvePublishedMessageType(
        InvocationExpressionSyntax invocation,
        IReadOnlyDictionary<string, string?> parameterTypes,
        Dictionary<string, string> localVariables,
        string? preferredAssembly,
        string? preferredProject)
    {
        if (invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is not { } argument)
        {
            return null;
        }

        return TryResolveExpressionType(argument, parameterTypes, localVariables, preferredAssembly, preferredProject);
    }
}
