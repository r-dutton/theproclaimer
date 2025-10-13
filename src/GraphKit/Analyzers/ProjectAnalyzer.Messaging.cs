using System;
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
    private void AnalyzePublisher(ProjectInfo project, Microsoft.CodeAnalysis.SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        string? queueOrTopic = null;
        string? subject = null;

        foreach (var invocation in classDeclaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
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

        foreach (var creation in classDeclaration.DescendantNodes().OfType<ObjectCreationExpressionSyntax>())
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
                .SelectMany(alias => new[] { alias, alias.Split('.').Last() })
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

        var simple = messageType.Split('.').Last();
        var existing = _messageContracts.Values.FirstOrDefault(c =>
            c.Fqdn.Equals(messageType, StringComparison.OrdinalIgnoreCase) ||
            c.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));

        if (existing is not null)
        {
            return existing;
        }

        var fqdn = messageType.Contains('.') ? messageType : simple;
        var symbolId = $"T:{fqdn}";
        return new MessageContractInfo(fqdn, string.Empty, string.Empty, string.Empty, new GraphSpan { StartLine = 1, EndLine = 1 }, symbolId, simple);
    }
}
