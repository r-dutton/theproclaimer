using GraphKit.Graph;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void EmitPublishers()
    {
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
                    ["queue"] = "reports.events"
                }
            };

            if (_messageContracts.Values.FirstOrDefault(contract => contract.Name.Contains("ReportPublished", StringComparison.Ordinal)) is { } contractInfo)
            {
                var contractId = StableId.For("message.contract", contractInfo.Fqdn, contractInfo.Assembly, contractInfo.SymbolId);
                _nodes[contractId] = new GraphNode
                {
                    Id = contractId,
                    Type = "message.contract",
                    Name = contractInfo.Name,
                    Fqdn = contractInfo.Fqdn,
                    Assembly = contractInfo.Assembly,
                    Project = contractInfo.Project,
                    FilePath = contractInfo.FilePath,
                    Span = contractInfo.Span,
                    SymbolId = contractInfo.SymbolId,
                    Tags = new[] { "messaging" }
                };

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

            foreach (var handler in _handlers.Values)
            {
                var publisherCall = handler.PublisherCalls.FirstOrDefault(call => call.PublisherType.Contains(publisher.Name, StringComparison.Ordinal));
                if (publisherCall is null)
                {
                    continue;
                }

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
                        Location = new GraphLocation { File = handler.FilePath, Line = publisherCall.Line }
                    },
                    Evidence = CreateEvidence(handler.FilePath, publisherCall.Line)
                });
            }
        }
    }
}
