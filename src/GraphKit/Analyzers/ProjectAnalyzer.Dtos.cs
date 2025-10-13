using GraphKit.Graph;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void EmitDtos()
    {
        foreach (var dto in _dtos.Values)
        {
            var id = StableId.For("dto", dto.Fqdn, dto.Assembly, dto.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "dto",
                Name = dto.Name,
                Fqdn = dto.Fqdn,
                Assembly = dto.Assembly,
                Project = dto.Project,
                FilePath = dto.FilePath,
                Span = dto.Span,
                SymbolId = dto.SymbolId,
                Tags = new[] { "app" }
            };
        }
    }

    private void EmitValidators()
    {
        foreach (var validator in _validators)
        {
            var id = StableId.For("validator", validator.Fqdn, validator.Assembly, validator.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "validator",
                Name = validator.Name,
                Fqdn = validator.Fqdn,
                Assembly = validator.Assembly,
                Project = validator.Project,
                FilePath = validator.FilePath,
                Span = validator.Span,
                SymbolId = validator.SymbolId,
                Tags = new[] { "app" }
            };

            if (_dtos.Values.FirstOrDefault(dto => dto.Name == validator.TargetType.Split('.').Last()) is { } dtoInfo)
            {
                var dtoId = StableId.For("dto", dtoInfo.Fqdn, dtoInfo.Assembly, dtoInfo.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = dtoId,
                    To = id,
                    Kind = "validates",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "validation",
                        Location = new GraphLocation { File = validator.FilePath, Line = validator.Span.StartLine }
                    },
                    Evidence = CreateEvidence(validator.FilePath, validator.Span)
                });
            }
        }
    }
}
