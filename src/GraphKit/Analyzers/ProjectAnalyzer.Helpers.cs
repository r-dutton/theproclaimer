using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GraphKit.Constants;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Graph;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private static GenericNameSyntax? MatchGenericInterface(TypeSyntax typeSyntax, params string[] interfaceNames)
    {
        if (typeSyntax is GenericNameSyntax generic)
        {
            foreach (var name in interfaceNames)
            {
                if (generic.Identifier.Text.Equals(name, StringComparison.Ordinal))
                {
                    return generic;
                }
            }

            return null;
        }

        if (typeSyntax is QualifiedNameSyntax qualified)
        {
            return MatchGenericInterface(qualified.Right, interfaceNames);
        }

        if (typeSyntax is AliasQualifiedNameSyntax alias)
        {
            return MatchGenericInterface(alias.Name, interfaceNames);
        }

        return null;
    }

    private bool TryResolveNodeReference(string typeName, out NodeReference reference, string? preferredAssembly = null, string? preferredProject = null)
    {
        var simple = GetSimpleIdentifier(typeName);
        var typeAssemblyRoot = GetTypeAssemblyRoot(typeName);

        if (TryResolveEntityNodeReference(typeName, out reference, preferredAssembly, preferredProject))
        {
            return true;
        }

        if (_dtos.TryGetValue(typeName, out var dto))
        {
            var id = StableId.For("dto", dto.Fqdn, dto.Assembly, dto.SymbolId);
            reference = new NodeReference(id, dto.FilePath, dto.Span);
            return true;
        }

        var dtoFallback = SelectBestCandidate(
            _dtos.Values.Where(d => d.Name.Equals(simple, StringComparison.Ordinal)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            d => d.Assembly,
            d => d.Project,
            d => d.Fqdn);

        if (dtoFallback is not null)
        {
            var id = StableId.For("dto", dtoFallback.Fqdn, dtoFallback.Assembly, dtoFallback.SymbolId);
            reference = new NodeReference(id, dtoFallback.FilePath, dtoFallback.Span);
            return true;
        }

        if (_requests.TryGetValue(typeName, out var request))
        {
            var id = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
            reference = new NodeReference(id, request.FilePath, request.Span);
            return true;
        }

        var requestFallback = SelectBestCandidate(
            _requests.Values.Where(r => r.Name.Equals(simple, StringComparison.Ordinal)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            r => r.Assembly,
            r => r.Project,
            r => r.Fqdn);

        if (requestFallback is not null)
        {
            var id = StableId.For("cqrs.request", requestFallback.Fqdn, requestFallback.Assembly, requestFallback.SymbolId);
            reference = new NodeReference(id, requestFallback.FilePath, requestFallback.Span);
            return true;
        }

        if (_notifications.TryGetValue(typeName, out var notification))
        {
            var id = StableId.For("cqrs.notification", notification.Fqdn, notification.Assembly, notification.SymbolId);
            reference = new NodeReference(id, notification.FilePath, notification.Span);
            return true;
        }

        var notificationFallback = SelectBestCandidate(
            _notifications.Values.Where(n => n.Name.Equals(simple, StringComparison.Ordinal)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            n => n.Assembly,
            n => n.Project,
            n => n.Fqdn);

        if (notificationFallback is not null)
        {
            var id = StableId.For("cqrs.notification", notificationFallback.Fqdn, notificationFallback.Assembly, notificationFallback.SymbolId);
            reference = new NodeReference(id, notificationFallback.FilePath, notificationFallback.Span);
            return true;
        }

        if (_dbContexts.TryGetValue(typeName, out var dbContext))
        {
            var id = StableId.For("ef.db_context", dbContext.Fqdn, dbContext.Assembly, dbContext.SymbolId);
            reference = new NodeReference(id, dbContext.FilePath, dbContext.Span);
            return true;
        }

        var contextFallback = SelectBestCandidate(
            _dbContexts.Values.Where(c =>
                c.Fqdn.Equals(typeName, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            c => c.Assembly,
            c => c.Project,
            c => c.Fqdn);

        if (contextFallback is not null)
        {
            var id = StableId.For("ef.db_context", contextFallback.Fqdn, contextFallback.Assembly, contextFallback.SymbolId);
            reference = new NodeReference(id, contextFallback.FilePath, contextFallback.Span);
            return true;
        }

        if (_backgroundServices.TryGetValue(typeName, out var backgroundService))
        {
            var id = StableId.For("app.background_service", backgroundService.Fqdn, backgroundService.Assembly, backgroundService.SymbolId);
            reference = new NodeReference(id, backgroundService.FilePath, backgroundService.Span);
            return true;
        }

        var backgroundFallback = SelectBestCandidate(
            _backgroundServices.Values.Where(s =>
                s.Fqdn.Equals(typeName, StringComparison.OrdinalIgnoreCase) ||
                s.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            s => s.Assembly,
            s => s.Project,
            s => s.Fqdn);

        if (backgroundFallback is not null)
        {
            var id = StableId.For("app.background_service", backgroundFallback.Fqdn, backgroundFallback.Assembly, backgroundFallback.SymbolId);
            reference = new NodeReference(id, backgroundFallback.FilePath, backgroundFallback.Span);
            return true;
        }

        if (_handlers.TryGetValue(typeName, out var handler))
        {
            var id = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
            reference = new NodeReference(id, handler.FilePath, handler.Span);
            return true;
        }

        var handlerFallback = SelectBestCandidate(
            _handlers.Values.Where(h => h.Name.Equals(simple, StringComparison.Ordinal)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            h => h.Assembly,
            h => h.Project,
            h => h.Fqdn);

        if (handlerFallback is not null)
        {
            var id = StableId.For("cqrs.handler", handlerFallback.Fqdn, handlerFallback.Assembly, handlerFallback.SymbolId);
            reference = new NodeReference(id, handlerFallback.FilePath, handlerFallback.Span);
            return true;
        }

        if (_pipelineBehaviors.TryGetValue(typeName, out var pipeline))
        {
            var id = StableId.For("cqrs.pipeline_behavior", pipeline.Fqdn, pipeline.Assembly, pipeline.SymbolId);
            reference = new NodeReference(id, pipeline.FilePath, pipeline.Span);
            return true;
        }

        var pipelineFallback = SelectBestCandidate(
            _pipelineBehaviors.Values.Where(p => p.Name.Equals(simple, StringComparison.Ordinal)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            p => p.Assembly,
            p => p.Project,
            p => p.Fqdn);

        if (pipelineFallback is not null)
        {
            var id = StableId.For("cqrs.pipeline_behavior", pipelineFallback.Fqdn, pipelineFallback.Assembly, pipelineFallback.SymbolId);
            reference = new NodeReference(id, pipelineFallback.FilePath, pipelineFallback.Span);
            return true;
        }

        if (_services.TryGetValue(typeName, out var service))
        {
            var id = StableId.For("app.service", service.Fqdn, service.Assembly, service.SymbolId);
            reference = new NodeReference(id, service.FilePath, service.Span);
            return true;
        }

        var serviceFallback = SelectBestCandidate(
            _services.Values.Where(s =>
                s.Fqdn.Equals(typeName, StringComparison.OrdinalIgnoreCase) ||
                s.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            s => s.Assembly,
            s => s.Project,
            s => s.Fqdn);

        if (serviceFallback is not null)
        {
            var id = StableId.For("app.service", serviceFallback.Fqdn, serviceFallback.Assembly, serviceFallback.SymbolId);
            reference = new NodeReference(id, serviceFallback.FilePath, serviceFallback.Span);
            return true;
        }

        if (_requestProcessors.TryGetValue(typeName, out var processor))
        {
            var id = StableId.For("cqrs.request_processor", processor.Fqdn, processor.Assembly, processor.SymbolId);
            reference = new NodeReference(id, processor.FilePath, processor.Span);
            return true;
        }

        var processorFallback = SelectBestCandidate(
            _requestProcessors.Values.Where(p => p.Name.Equals(simple, StringComparison.Ordinal)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            p => p.Assembly,
            p => p.Project,
            p => p.Fqdn);

        if (processorFallback is not null)
        {
            var id = StableId.For("cqrs.request_processor", processorFallback.Fqdn, processorFallback.Assembly, processorFallback.SymbolId);
            reference = new NodeReference(id, processorFallback.FilePath, processorFallback.Span);
            return true;
        }

        if (_repositories.TryGetValue(typeName, out var repository))
        {
            var id = StableId.For("app.repository", repository.Fqdn, repository.Assembly, repository.SymbolId);
            reference = new NodeReference(id, repository.FilePath, repository.Span);
            return true;
        }

        var repositoryFallback = SelectBestCandidate(
            _repositories.Values.Where(r => r.Name.Equals(simple, StringComparison.Ordinal)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            r => r.Assembly,
            r => r.Project,
            r => r.Fqdn);

        if (repositoryFallback is not null)
        {
            var id = StableId.For("app.repository", repositoryFallback.Fqdn, repositoryFallback.Assembly, repositoryFallback.SymbolId);
            reference = new NodeReference(id, repositoryFallback.FilePath, repositoryFallback.Span);
            return true;
        }

        if (ResolveImplementationType(typeName) is { } resolved && !string.Equals(resolved, typeName, StringComparison.Ordinal))
        {
            return TryResolveNodeReference(resolved, out reference, preferredAssembly, preferredProject);
        }

        reference = default!;
        return false;
    }

    /// <summary>
    /// Resolve a repository info record for the given type name, preferring exact matches and
    /// then falling back to name-based selection with the same scoring rules used for node resolution.
    /// </summary>
    private bool TryResolveRepositoryInfo(string repositoryType, out RepositoryInfo repository, string? preferredAssembly = null, string? preferredProject = null)
    {
        repository = default!;
        if (string.IsNullOrWhiteSpace(repositoryType))
        {
            return false;
        }

        if (_repositories.TryGetValue(repositoryType, out repository))
        {
            return true;
        }

        var simple = GetSimpleIdentifier(repositoryType);
        var typeAssemblyRoot = GetTypeAssemblyRoot(repositoryType);

        var candidate = SelectBestCandidate(
            _repositories.Values.Where(r => r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            r => r.Assembly,
            r => r.Project,
            r => r.Fqdn);

        if (candidate is not null)
        {
            repository = candidate;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Resolve the entity type for a repository call, preferring symbol-bound RepositoryInfo.EntityTypeFqdn
    /// and using per-invocation generic/argument hints, with legacy string heuristics as a final fallback.
    /// </summary>
    private string? ResolveRepositoryEntityType(
        string? repositoryType,
        string? originalRepositoryType,
        string? preferredAssembly,
        string? preferredProject,
        SimpleNameSyntax? methodNameSyntax = null,
        InvocationExpressionSyntax? invocation = null)
    {
        string? entityType = null;

        if (!string.IsNullOrWhiteSpace(repositoryType) &&
            TryResolveRepositoryInfo(repositoryType!, out var repository, preferredAssembly, preferredProject))
        {
            entityType = repository.EntityTypeFqdn;
        }

        if (string.IsNullOrWhiteSpace(entityType) &&
            !string.IsNullOrWhiteSpace(originalRepositoryType) &&
            !string.Equals(repositoryType, originalRepositoryType, StringComparison.OrdinalIgnoreCase) &&
            TryResolveRepositoryInfo(originalRepositoryType!, out var originalRepository, preferredAssembly, preferredProject))
        {
            entityType = originalRepository.EntityTypeFqdn;
        }

        if (methodNameSyntax is not null && invocation is not null)
        {
            var invocationEntityType = ExtractRepositoryEntityTypeFromInvocation(methodNameSyntax, invocation);
            if (!string.IsNullOrWhiteSpace(invocationEntityType))
            {
                entityType = invocationEntityType;
            }
        }

        if (string.IsNullOrWhiteSpace(entityType))
        {
            entityType = ExtractRepositoryEntityType(repositoryType)
                ?? ExtractRepositoryEntityType(originalRepositoryType)
                ?? TryDeriveEntityTypeFromRepositoryName(repositoryType)
                ?? TryDeriveEntityTypeFromRepositoryName(originalRepositoryType);
        }

        if (!string.IsNullOrWhiteSpace(entityType))
        {
            entityType = QualifyTypeName(entityType!, preferredAssembly, preferredProject);
        }

        return entityType;
    }

    private bool TryResolveEntityNodeReference(string typeName, out NodeReference reference, string? preferredAssembly, string? preferredProject)
    {
        reference = default;
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        if (_entities.TryGetValue(typeName, out var entity))
        {
            var id = StableId.For("ef.entity", entity.Fqdn, entity.Assembly, entity.SymbolId);
            reference = new NodeReference(id, entity.FilePath, entity.Span);
            return true;
        }

        var simple = GetSimpleIdentifier(typeName);
        var typeAssemblyRoot = GetTypeAssemblyRoot(typeName);
        var entityFallback = SelectBestCandidate(
            _entities.Values.Where(e => e.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)),
            preferredAssembly,
            preferredProject,
            typeAssemblyRoot,
            e => e.Assembly,
            e => e.Project,
            e => e.Fqdn);

        if (entityFallback is not null)
        {
            var id = StableId.For("ef.entity", entityFallback.Fqdn, entityFallback.Assembly, entityFallback.SymbolId);
            reference = new NodeReference(id, entityFallback.FilePath, entityFallback.Span);
            return true;
        }

        return false;
    }

    private static T? SelectBestCandidate<T>(
        IEnumerable<T> candidates,
        string? preferredAssembly,
        string? preferredProject,
        string? typeAssemblyRoot,
        Func<T, string> getAssembly,
        Func<T, string> getProject,
        Func<T, string> getFqdn)
    {
        if (candidates is null)
        {
            return default;
        }

        var list = candidates.ToList();
        if (list.Count == 0)
        {
            return default;
        }

        if (list.Count == 1)
        {
            return list[0];
        }

        var preferredAssemblyRoot = GetAssemblyRoot(preferredAssembly);
        var preferredProjectRoot = GetProjectRoot(preferredProject);

        var nonTestCandidates = list
            .Where(candidate => !IsTestProject(getProject(candidate)))
            .ToList();

        if (nonTestCandidates.Count > 0)
        {
            list = nonTestCandidates;
        }
        else
        {
            var productionLike = list
                .Where(candidate => !LooksLikeTestImplementation(getFqdn(candidate)))
                .ToList();
            if (productionLike.Count > 0)
            {
                list = productionLike;
            }
        }

        T? best = default;
        var bestScore = int.MinValue;

        foreach (var candidate in list)
        {
            var score = 0;
            var candidateAssembly = getAssembly(candidate);
            var candidateProject = getProject(candidate);
            var candidateAssemblyRoot = GetAssemblyRoot(candidateAssembly);
            var candidateProjectRoot = GetProjectRoot(candidateProject);

            if (!string.IsNullOrWhiteSpace(preferredAssembly) &&
                string.Equals(candidateAssembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
            {
                score += 500;
            }

            if (!string.IsNullOrWhiteSpace(preferredAssemblyRoot) &&
                string.Equals(candidateAssemblyRoot, preferredAssemblyRoot, StringComparison.OrdinalIgnoreCase))
            {
                score += 400;
            }

            if (!string.IsNullOrWhiteSpace(typeAssemblyRoot) &&
                string.Equals(candidateAssemblyRoot, typeAssemblyRoot, StringComparison.OrdinalIgnoreCase))
            {
                score += 350;
            }

            if (!string.IsNullOrWhiteSpace(preferredProject) &&
                string.Equals(candidateProject, preferredProject, StringComparison.OrdinalIgnoreCase))
            {
                score += 250;
            }

            if (!string.IsNullOrWhiteSpace(preferredProjectRoot) &&
                string.Equals(candidateProjectRoot, preferredProjectRoot, StringComparison.OrdinalIgnoreCase))
            {
                score += 200;
            }

            if (IsTestProject(candidateProject))
            {
                score -= 400;
            }

            if (best is null || score > bestScore ||
                (score == bestScore && string.Compare(getFqdn(candidate), getFqdn(best), StringComparison.OrdinalIgnoreCase) < 0))
            {
                best = candidate;
                bestScore = score;
            }
        }

        return best ?? list
            .OrderBy(candidate => getFqdn(candidate), StringComparer.OrdinalIgnoreCase)
            .First();
    }

    private HandlerInfo? FindHandlerForRequest(string requestType)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return null;
        }

        if (_handlersByRequestType.TryGetValue(requestType, out var handler))
        {
            return handler;
        }

        if (_handlers.TryGetValue(requestType, out handler))
        {
            return handler;
        }

        var simple = GetSimpleIdentifier(requestType);

        var matches = _handlersByRequestType
            .Where(kv => GetSimpleIdentifier(kv.Key).Equals(simple, StringComparison.OrdinalIgnoreCase))
            .Select(kv => kv.Value)
            .Distinct()
            .ToList();

        if (matches.Count == 1)
        {
            return matches[0];
        }

        matches = _handlers.Values
            .Where(h => h.RequestSignatures.Any(sig =>
                sig.RequestType.Equals(requestType, StringComparison.OrdinalIgnoreCase) ||
                GetSimpleIdentifier(sig.RequestType).Equals(simple, StringComparison.OrdinalIgnoreCase)))
            .Distinct()
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private RequestInfo? FindRequestByType(string requestType, string? preferredAssembly = null, string? preferredProject = null, string? serviceType = null)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return null;
        }

        if (_requests.TryGetValue(requestType, out var request))
        {
            return request;
        }

        var candidates = new Dictionary<string, RequestInfo>(StringComparer.OrdinalIgnoreCase);
        var simple = GetSimpleIdentifier(requestType);

        foreach (var match in _requests.Values.Where(r =>
                     r.Fqdn.Equals(requestType, StringComparison.OrdinalIgnoreCase) ||
                     r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)))
        {
            candidates[match.Fqdn] = match;
        }

        foreach (var match in FindRequestsByInterface(requestType))
        {
            candidates[match.Fqdn] = match;
        }

        if (candidates.Count == 0)
        {
            return null;
        }

        if (candidates.Count == 1)
        {
            return candidates.Values.First();
        }

        var candidateList = candidates.Values.ToList();

        if (!string.IsNullOrWhiteSpace(serviceType))
        {
            var serviceRoot = GetTypeAssemblyRoot(serviceType);
            if (!string.IsNullOrWhiteSpace(serviceRoot))
            {
                var serviceMatches = candidateList
                    .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), serviceRoot, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (serviceMatches.Count == 1)
                {
                    return serviceMatches[0];
                }

                if (serviceMatches.Count > 1)
                {
                    candidateList = serviceMatches;
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(preferredAssembly))
        {
            var assemblyMatches = candidateList
                .Where(r => string.Equals(r.Assembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (assemblyMatches.Count == 1)
            {
                return assemblyMatches[0];
            }

            if (assemblyMatches.Count > 1)
            {
                candidateList = assemblyMatches;
            }

            if (assemblyMatches.Count == 0)
            {
                var preferredRoot = GetAssemblyRoot(preferredAssembly);
                if (!string.IsNullOrWhiteSpace(preferredRoot))
                {
                    var rootMatches = candidateList
                        .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), preferredRoot, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (rootMatches.Count == 1)
                    {
                        return rootMatches[0];
                    }

                    if (rootMatches.Count > 1)
                    {
                        candidateList = rootMatches;
                    }
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(preferredProject))
        {
            var projectMatches = candidateList
                .Where(r => string.Equals(r.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (projectMatches.Count == 1)
            {
                return projectMatches[0];
            }

            if (projectMatches.Count > 1)
            {
                candidateList = projectMatches;
            }
        }

        var interfaceKeys = new HashSet<string>(DeriveInterfaceLookupKeys(requestType), StringComparer.OrdinalIgnoreCase);
        if (interfaceKeys.Count > 0)
        {
            var interfaceMatches = new List<RequestInfo>();
            foreach (var candidate in candidateList)
            {
                if (candidate.Interfaces.Count == 0)
                {
                    continue;
                }

                var matched = false;
                foreach (var implemented in candidate.Interfaces)
                {
                    foreach (var key in DeriveInterfaceLookupKeys(implemented))
                    {
                        if (!string.IsNullOrWhiteSpace(key) && interfaceKeys.Contains(key))
                        {
                            interfaceMatches.Add(candidate);
                            matched = true;
                            break;
                        }
                    }

                    if (matched)
                    {
                        break;
                    }
                }
            }

            if (interfaceMatches.Count == 1)
            {
                return interfaceMatches[0];
            }

            if (interfaceMatches.Count > 1)
            {
                candidateList = interfaceMatches;
            }
        }

        var requestNamespace = GetTypeNamespace(requestType);
        if (!string.IsNullOrWhiteSpace(requestNamespace))
        {
            var ordered = candidateList
                .Select(r => new { Item = r, Score = LongestCommonPrefixLength(requestNamespace, r.Fqdn) })
                .OrderByDescending(x => x.Score)
                .ThenBy(x => x.Item.Fqdn, StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (ordered.Count > 0 && ordered[0].Score > 0)
            {
                var nextScore = ordered.ElementAtOrDefault(1)?.Score ?? int.MinValue;
                if (ordered.Count == 1 || ordered[0].Score > nextScore)
                {
                    return ordered[0].Item;
                }
            }
        }

        var nonTest = candidateList
            .Where(r => !r.Assembly.Contains(".Tests", StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (nonTest.Count == 1)
        {
            return nonTest[0];
        }

        return candidateList
            .OrderBy(r => r.Fqdn, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
    }

    private IReadOnlyList<RequestInfo> FindRequestsByInterface(string interfaceType)
    {
        if (string.IsNullOrWhiteSpace(interfaceType))
        {
            return Array.Empty<RequestInfo>();
        }

        var keys = DeriveInterfaceLookupKeys(interfaceType);
        if (keys.Count == 0)
        {
            return Array.Empty<RequestInfo>();
        }

        var matches = new Dictionary<string, RequestInfo>(StringComparer.OrdinalIgnoreCase);
        foreach (var key in keys)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            if (_requestsByInterfaceType.TryGetValue(key, out var lookup))
            {
                foreach (var pair in lookup)
                {
                    matches[pair.Key] = pair.Value;
                }
            }
        }

        return matches.Count == 0 ? Array.Empty<RequestInfo>() : matches.Values.ToList();
    }

    private PipelineBehaviorInfo? FindPipelineBehavior(string behaviorType)
    {
        if (string.IsNullOrWhiteSpace(behaviorType))
        {
            return null;
        }

        if (_pipelineBehaviors.TryGetValue(behaviorType, out var info))
        {
            return info;
        }

        var baseType = GetTypeNameWithoutGenerics(behaviorType);
        if (!string.Equals(baseType, behaviorType, StringComparison.Ordinal) &&
            _pipelineBehaviors.TryGetValue(baseType, out info))
        {
            return info;
        }

        var simple = GetSimpleIdentifier(baseType);
        var matches = _pipelineBehaviors.Values
            .Where(b =>
                b.Fqdn.Equals(behaviorType, StringComparison.OrdinalIgnoreCase) ||
                b.Fqdn.Equals(baseType, StringComparison.OrdinalIgnoreCase) ||
                b.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private RequestProcessorInfo? FindRequestProcessor(string processorType)
    {
        if (string.IsNullOrWhiteSpace(processorType))
        {
            return null;
        }

        if (_requestProcessors.TryGetValue(processorType, out var info))
        {
            return info;
        }

        var baseType = GetTypeNameWithoutGenerics(processorType);
        if (!string.Equals(baseType, processorType, StringComparison.Ordinal) &&
            _requestProcessors.TryGetValue(baseType, out info))
        {
            return info;
        }

        var simple = GetSimpleIdentifier(baseType);
        var matches = _requestProcessors.Values
            .Where(p =>
                p.Fqdn.Equals(processorType, StringComparison.OrdinalIgnoreCase) ||
                p.Fqdn.Equals(baseType, StringComparison.OrdinalIgnoreCase) ||
                p.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    internal bool TryResolveRequestDispatch(
        FlowPointsToFacade pointsTo,
        IInvocationOperation invocation,
        string receiverTypeName,
        IReadOnlyCollection<string>? implementations,
        string assembly,
        string project,
        out string? targetType,
        out string? requestType,
        out string? responseType,
        out string? dispatchKind)
    {
        targetType = null;
        requestType = null;
        responseType = null;
        dispatchKind = null;

        if (!IsRequestProcessorMethod(invocation.TargetMethod?.Name))
        {
            return false;
        }

        if (!IsRequestProcessorCandidate(receiverTypeName, implementations))
        {
            return false;
        }

        dispatchKind = EdgeKinds.RequestProcessorDispatch;

        var requestArgument = invocation.Arguments.Length > 0 ? invocation.Arguments[0].Value : null;
        requestType = TryResolveConcreteType(pointsTo, requestArgument, assembly, project);

        var processorInfo = TryResolveProcessorInfo(receiverTypeName, implementations);
        string? metadataResponse = null;

        if (processorInfo is not null)
        {
            metadataResponse = NormalizeTypeName(processorInfo.ResponseType, assembly, project);
            var processorRequest = NormalizeTypeName(processorInfo.RequestType, assembly, project);
            if (string.IsNullOrWhiteSpace(requestType))
            {
                requestType = processorRequest;
            }
        }

        if (string.IsNullOrWhiteSpace(requestType))
        {
            requestType = TryExtractRequestTypeFromCandidates(receiverTypeName, implementations, assembly, project, out var genericResponse);
            metadataResponse ??= genericResponse;
        }

        if (string.IsNullOrWhiteSpace(requestType))
        {
            return false;
        }

        targetType = requestType;
        EnsureHandlerAnalysis(requestType);

        responseType = metadataResponse;

        var requestInfo = FindRequestByType(requestType, assembly, project, receiverTypeName);
        if (requestInfo is not null)
        {
            var requestResponse = NormalizeTypeName(requestInfo.ResponseType, assembly, project);
            if (!string.IsNullOrWhiteSpace(requestResponse))
            {
                responseType = requestResponse;
            }
        }

        if (string.IsNullOrWhiteSpace(responseType) && processorInfo is not null)
        {
            responseType = NormalizeTypeName(processorInfo.ResponseType, assembly, project);
        }

        if (string.IsNullOrWhiteSpace(responseType))
        {
            responseType = ResolveResponseFromMethod(invocation.TargetMethod, assembly, project);
        }

        return true;
    }

    private static bool IsRequestProcessorMethod(string? methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return false;
        }

        return methodName.Equals("Process", StringComparison.OrdinalIgnoreCase) ||
               methodName.Equals("ProcessAsync", StringComparison.OrdinalIgnoreCase);
    }

    private bool IsRequestProcessorCandidate(string serviceTypeName, IReadOnlyCollection<string>? implementations)
    {
        if (ContainsRequestProcessor(serviceTypeName))
        {
            return true;
        }

        if (implementations is not null)
        {
            foreach (var implementation in implementations)
            {
                if (ContainsRequestProcessor(implementation))
                {
                    return true;
                }
            }
        }

        if (FindRequestProcessor(serviceTypeName) is not null)
        {
            return true;
        }

        if (implementations is not null)
        {
            foreach (var implementation in implementations)
            {
                if (FindRequestProcessor(implementation) is not null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool ContainsRequestProcessor(string? candidate)
    {
        if (string.IsNullOrWhiteSpace(candidate))
        {
            return false;
        }

        return candidate.IndexOf("RequestProcessor", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private RequestProcessorInfo? TryResolveProcessorInfo(string serviceTypeName, IReadOnlyCollection<string>? implementations)
    {
        if (FindRequestProcessor(serviceTypeName) is { } processor)
        {
            return processor;
        }

        if (implementations is not null)
        {
            foreach (var implementation in implementations)
            {
                if (FindRequestProcessor(implementation) is { } match)
                {
                    return match;
                }
            }
        }

        return null;
    }

    private string? TryExtractRequestTypeFromCandidates(
        string receiverTypeName,
        IReadOnlyCollection<string>? implementations,
        string assembly,
        string project,
        out string? responseType)
    {
        responseType = null;

        foreach (var candidate in EnumerateRequestProcessorCandidates(receiverTypeName, implementations))
        {
            var genericArguments = SplitGenericArguments(candidate);
            if (genericArguments.Count == 0)
            {
                continue;
            }

            var requestCandidate = NormalizeTypeName(genericArguments[0], assembly, project);
            if (string.IsNullOrWhiteSpace(requestCandidate))
            {
                continue;
            }

            if (genericArguments.Count > 1)
            {
                responseType ??= NormalizeTypeName(genericArguments[1], assembly, project);
            }

            return requestCandidate;
        }

        return null;
    }

    private IEnumerable<string> EnumerateRequestProcessorCandidates(string receiverTypeName, IReadOnlyCollection<string>? implementations)
    {
        if (!string.IsNullOrWhiteSpace(receiverTypeName))
        {
            yield return receiverTypeName;
        }

        if (implementations is null)
        {
            yield break;
        }

        foreach (var implementation in implementations)
        {
            if (!string.IsNullOrWhiteSpace(implementation))
            {
                yield return implementation;
            }
        }
    }

    private string? TryResolveConcreteType(
        FlowPointsToFacade pointsTo,
        IOperation? operation,
        string assembly,
        string project)
    {
        if (operation is null)
        {
            return null;
        }

        if (operation is IConversionOperation conversion)
        {
            return TryResolveConcreteType(pointsTo, conversion.Operand, assembly, project);
        }

        var pointed = pointsTo.TryGetLocationTypes(operation);
        if (!pointed.IsDefaultOrEmpty)
        {
            string? fallback = null;
            foreach (var candidate in pointed)
            {
                var resolved = QualifySymbol(candidate, assembly, project);
                if (!IsMeaningfulType(resolved))
                {
                    continue;
                }

                if (candidate.TypeKind is TypeKind.Class or TypeKind.Struct)
                {
                    return resolved;
                }

                fallback ??= resolved;
            }

            if (!string.IsNullOrWhiteSpace(fallback))
            {
                return fallback;
            }
        }

        var typeFallback = QualifySymbol(operation.Type, assembly, project);
        return IsMeaningfulType(typeFallback) ? typeFallback : null;
    }

    private string? ResolveResponseFromMethod(IMethodSymbol? methodSymbol, string assembly, string project)
    {
        if (methodSymbol is null)
        {
            return null;
        }

        if (methodSymbol.IsGenericMethod)
        {
            foreach (var typeArgument in methodSymbol.TypeArguments)
            {
                var resolved = QualifySymbol(typeArgument, assembly, project);
                if (IsMeaningfulType(resolved))
                {
                    return resolved;
                }
            }
        }

        var returnType = methodSymbol.ReturnType;
        if (returnType is INamedTypeSymbol named && named.TypeArguments.Length == 1 && IsTaskLike(named))
        {
            var resolved = QualifySymbol(named.TypeArguments[0], assembly, project);
            if (IsMeaningfulType(resolved))
            {
                return resolved;
            }
        }
        else
        {
            var resolved = QualifySymbol(returnType, assembly, project);
            if (IsMeaningfulType(resolved))
            {
                return resolved;
            }
        }

        return null;
    }

    private string? QualifySymbol(ITypeSymbol? symbol, string assembly, string project)
    {
        if (symbol is null)
        {
            return null;
        }

        var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        var qualified = QualifyTypeName(display, assembly, project);
        return string.IsNullOrWhiteSpace(qualified) ? display : qualified;
    }

    private string? NormalizeTypeName(string? typeName, string assembly, string project)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var qualified = QualifyTypeName(typeName, assembly, project);
        var candidate = string.IsNullOrWhiteSpace(qualified) ? typeName : qualified;
        return IsMeaningfulType(candidate) ? candidate : null;
    }

    private static bool IsMeaningfulType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        if (IsVoidLike(typeName))
        {
            return false;
        }

        return !IsGenericPlaceholder(typeName);
    }

    private static bool IsVoidLike(string typeName)
    {
        return typeName.Equals("void", StringComparison.OrdinalIgnoreCase) ||
               typeName.Equals("System.Void", StringComparison.OrdinalIgnoreCase) ||
               typeName.Equals("Unit", StringComparison.OrdinalIgnoreCase) ||
               typeName.Equals("MediatR.Unit", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsTaskLike(INamedTypeSymbol symbol)
    {
        if (symbol is null)
        {
            return false;
        }

        if (symbol.Name is not ("Task" or "ValueTask"))
        {
            return false;
        }

        var ns = symbol.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        return string.Equals(ns, "System.Threading.Tasks", StringComparison.Ordinal);
    }

    private IReadOnlyList<string> ResolvePipelineBehaviorsForRequest(string requestType)
    {
        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (!string.IsNullOrWhiteSpace(requestType) &&
            _requestPipelineRegistrations.TryGetValue(requestType, out var registrations))
        {
            foreach (var behaviorType in registrations)
            {
                var behavior = FindPipelineBehavior(behaviorType);
                if (behavior is not null)
                {
                    result.Add(behavior.Name);
                }
                else
                {
                    var simple = GetSimpleIdentifier(GetTypeNameWithoutGenerics(behaviorType));
                    if (!string.IsNullOrWhiteSpace(simple))
                    {
                        result.Add(simple);
                    }
                }
            }
        }

        foreach (var behavior in _pipelineBehaviors.Values)
        {
            if (behavior.RegisteredRequestTypes.Contains(requestType))
            {
                result.Add(behavior.Name);
                continue;
            }

            if (behavior.RegisteredRequestTypes.Count == 0)
            {
                result.Add(behavior.Name);
            }
        }

        foreach (var behaviorType in _globalPipelineBehaviors.Keys)
        {
            var behavior = FindPipelineBehavior(behaviorType);
            if (behavior is not null)
            {
                result.Add(behavior.Name);
            }
            else
            {
                var simple = GetSimpleIdentifier(GetTypeNameWithoutGenerics(behaviorType));
                if (!string.IsNullOrWhiteSpace(simple))
                {
                    result.Add(simple);
                }
            }
        }

        return result.Count == 0
            ? Array.Empty<string>()
            : result
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
    }

    private NotificationInfo? FindNotificationByType(string notificationType)
    {
        if (_notifications.TryGetValue(notificationType, out var notification))
        {
            return notification;
        }

        var simple = GetSimpleIdentifier(notificationType);

        var matches = _notifications.Values
            .Where(n =>
                n.Fqdn.Equals(notificationType, StringComparison.OrdinalIgnoreCase) ||
                n.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private DomainEventInfo? FindDomainEventByType(string domainEventType)
    {
        if (string.IsNullOrWhiteSpace(domainEventType))
        {
            return null;
        }

        if (_domainEvents.TryGetValue(domainEventType, out var domainEvent))
        {
            return domainEvent;
        }

        var baseType = GetTypeNameWithoutGenerics(domainEventType);
        if (!string.Equals(baseType, domainEventType, StringComparison.OrdinalIgnoreCase) &&
            _domainEvents.TryGetValue(baseType, out domainEvent))
        {
            return domainEvent;
        }

        var simple = GetSimpleIdentifier(domainEventType);
        var matches = _domainEvents.Values
            .Where(e =>
                e.Fqdn.Equals(domainEventType, StringComparison.OrdinalIgnoreCase) ||
                e.Fqdn.Equals(baseType, StringComparison.OrdinalIgnoreCase) ||
                e.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private string? ResolveImplementationType(string typeName, string? preferredAssembly = null, string? preferredProject = null)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var registration = FindServiceRegistration(typeName, preferredAssembly: preferredAssembly, preferredProject: preferredProject);
        if (registration is not null && !string.IsNullOrWhiteSpace(registration.ImplementationType))
        {
            if (!string.IsNullOrWhiteSpace(preferredAssembly))
            {
                var implementationRoot = GetAssemblyRootFromTypeName(registration.ImplementationType);
                var preferredRoot = GetAssemblyRoot(preferredAssembly);
                if (!string.IsNullOrWhiteSpace(preferredRoot) &&
                    !string.Equals(implementationRoot, preferredRoot, StringComparison.OrdinalIgnoreCase))
                {
                    var preferredImplementation = ResolveServiceByPreferences(typeName, preferredAssembly, preferredProject);
                    if (!string.IsNullOrWhiteSpace(preferredImplementation))
                    {
                        return preferredImplementation;
                    }
                }
            }

            return registration.ImplementationType;
        }

        if (TryResolveGenericImplementation(typeName, out var implementationType, preferredAssembly, preferredProject))
        {
            return implementationType;
        }

        if (!string.IsNullOrWhiteSpace(preferredAssembly) || !string.IsNullOrWhiteSpace(preferredProject))
        {
            var preferredImplementation = ResolveServiceByPreferences(typeName, preferredAssembly, preferredProject);
            if (!string.IsNullOrWhiteSpace(preferredImplementation))
            {
                return preferredImplementation;
            }
        }

        if (typeName.StartsWith("IControlledRepository", StringComparison.Ordinal))
        {
            var entity = SplitGenericArguments(typeName).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(entity))
            {
                var qualifiedEntity = QualifyTypeName(entity);
                var targetEntity = !string.IsNullOrWhiteSpace(qualifiedEntity) ? qualifiedEntity : entity;
                if (_repositories.Values.FirstOrDefault(r => r.ControlledEntities.Any(e =>
                        string.Equals(e, targetEntity, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(GetSimpleIdentifier(e), GetSimpleIdentifier(targetEntity), StringComparison.OrdinalIgnoreCase))) is { } repository)
                {
                    return repository.Fqdn;
                }
            }
        }

        return null;
    }

    private string? ResolveServiceByPreferences(string serviceType, string? preferredAssembly, string? preferredProject)
    {
        if (_services.Count == 0)
        {
            return null;
        }

        var simple = GetTopLevelSimpleIdentifier(serviceType);
        var preferredAssemblyRoot = GetAssemblyRoot(preferredAssembly);

        ServiceInfo? best = null;
        var bestScore = int.MinValue;

        foreach (var service in _services.Values)
        {
            if (!ServiceNameMatches(service, serviceType, simple))
            {
                continue;
            }

            var score = 0;

            if (string.Equals(service.Fqdn, serviceType, StringComparison.OrdinalIgnoreCase))
            {
                score += 600;
            }

            if (!string.IsNullOrWhiteSpace(simple) && string.Equals(service.Name, simple, StringComparison.OrdinalIgnoreCase))
            {
                score += 300;
            }

            if (!string.IsNullOrWhiteSpace(preferredAssembly))
            {
                if (string.Equals(service.Assembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
                {
                    score += 800;
                }
                else if (!string.IsNullOrWhiteSpace(preferredAssemblyRoot))
                {
                    var serviceRoot = GetAssemblyRoot(service.Assembly);
                    if (!string.IsNullOrWhiteSpace(serviceRoot) && string.Equals(serviceRoot, preferredAssemblyRoot, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 500;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(preferredProject) &&
                !string.IsNullOrWhiteSpace(service.Project) &&
                string.Equals(service.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
            {
                score += 400;
            }

            var serviceNamespace = GetTypeNamespace(service.Fqdn);
            var requestedNamespace = GetTypeNamespace(serviceType);
            if (!string.IsNullOrWhiteSpace(serviceNamespace) && !string.IsNullOrWhiteSpace(requestedNamespace))
            {
                score += LongestCommonPrefixLength(serviceNamespace, requestedNamespace);
            }

            if (best is null || score > bestScore)
            {
                best = service;
                bestScore = score;
            }
        }

        return best?.Fqdn;
    }

    private static bool ServiceNameMatches(ServiceInfo service, string serviceType, string? simple)
    {
        if (string.Equals(service.Fqdn, serviceType, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(service.Name, serviceType, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (!string.IsNullOrWhiteSpace(simple))
        {
            if (string.Equals(service.Name, simple, StringComparison.OrdinalIgnoreCase) ||
                service.Fqdn.EndsWith($".{simple}", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
            {
                var trimmed = simple[1..];
                if (string.Equals(service.Name, trimmed, StringComparison.OrdinalIgnoreCase) ||
                    service.Fqdn.EndsWith($".{trimmed}", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool TryResolveGenericImplementation(string typeName, out string? implementationType, string? preferredAssembly = null, string? preferredProject = null)
    {
        implementationType = null;
        if (!TryMakeOpenGenericType(typeName, out var openServiceType, out var typeArguments) || typeArguments.Count == 0)
        {
            return false;
        }

        var registration = FindServiceRegistration(openServiceType, preferredAssembly: preferredAssembly, preferredProject: preferredProject);
        if (registration is null)
        {
            var simpleOpen = GetSimpleIdentifier(openServiceType);
            registration = FindServiceRegistration(simpleOpen, preferredAssembly: preferredAssembly, preferredProject: preferredProject);
        }

        if (registration is null || string.IsNullOrWhiteSpace(registration.ImplementationType))
        {
            return false;
        }

        var implementationOpenType = registration.ImplementationType;
        if (!TryMakeOpenGenericType(implementationOpenType, out var implementationOpen, out _))
        {
            implementationType = registration.ImplementationType;
            return true;
        }

        var implementationArity = GetGenericArity(implementationOpen);
        if (implementationArity > 0 && implementationArity != typeArguments.Count)
        {
            implementationType = registration.ImplementationType;
            return true;
        }

        implementationType = CloseGenericType(implementationOpen, typeArguments);
        return true;
    }

    private static bool TryMakeOpenGenericType(string typeName, out string openType, out IReadOnlyList<string> typeArguments)
    {
        typeArguments = SplitGenericArguments(typeName);
        if (typeArguments.Count == 0)
        {
            openType = typeName;
            return false;
        }

        var genericStart = typeName.IndexOf('<');
        var genericEnd = typeName.LastIndexOf('>');
        if (genericStart < 0 || genericEnd <= genericStart)
        {
            openType = typeName;
            return false;
        }

        var builder = new StringBuilder();
        builder.Append(typeName[..genericStart]);
        builder.Append('<');
        for (var i = 0; i < typeArguments.Count; i++)
        {
            if (i > 0)
            {
                builder.Append(',');
            }
        }
        builder.Append('>');
        openType = builder.ToString();
        return true;
    }

    private static string CloseGenericType(string openType, IReadOnlyList<string> typeArguments)
    {
        if (!openType.Contains('<') || typeArguments.Count == 0)
        {
            return openType;
        }

        var prefix = openType[..openType.IndexOf('<')];
        var builder = new StringBuilder(prefix);
        builder.Append('<');
        for (var i = 0; i < typeArguments.Count; i++)
        {
            if (i > 0)
            {
                builder.Append(',');
            }

            builder.Append(typeArguments[i]);
        }
        builder.Append('>');
        return builder.ToString();
    }

    private string NormalizeServiceType(string serviceType, string? preferredAssembly = null, string? preferredProject = null)
    {
        if (string.IsNullOrWhiteSpace(serviceType))
        {
            return serviceType;
        }

        _services.TryGetValue(serviceType, out var directCandidate);

        if (TryResolveScopedService(serviceType, preferredAssembly, preferredProject, out var scopedFqdn))
        {
            return scopedFqdn;
        }

        var simple = GetSimpleIdentifier(serviceType);
        if (!string.IsNullOrWhiteSpace(simple) &&
            TryResolveScopedService(simple!, preferredAssembly, preferredProject, out var simpleScoped))
        {
            return simpleScoped;
        }

        var registration = FindServiceRegistration(serviceType, preferredTargetType: null, preferredAssembly, preferredProject)
            ?? (string.IsNullOrWhiteSpace(simple) ? null : FindServiceRegistration(simple!, preferredTargetType: null, preferredAssembly, preferredProject));

        if (registration is not null)
        {
            var implementation = registration.ImplementationType;
            if (!string.IsNullOrWhiteSpace(implementation) &&
                !string.Equals(implementation, serviceType, StringComparison.OrdinalIgnoreCase))
            {
                var nextAssembly = string.IsNullOrWhiteSpace(registration.Assembly) ? preferredAssembly : registration.Assembly;
                var nextProject = string.IsNullOrWhiteSpace(registration.Project) ? preferredProject : registration.Project;
                return NormalizeServiceType(implementation!, nextAssembly, nextProject);
            }
        }

        return directCandidate?.Fqdn ?? serviceType;
    }

    private bool TryResolveScopedService(string lookupKey, string? preferredAssembly, string? preferredProject, out string fqdn)
    {
        fqdn = lookupKey;
        if (string.IsNullOrWhiteSpace(lookupKey))
        {
            return false;
        }

        if (_services.TryGetValue(lookupKey, out var direct) &&
            MatchesServiceScope(direct, preferredAssembly, preferredProject))
        {
            fqdn = direct.Fqdn;
            return true;
        }

        var matches = _services.Values
            .Where(s => string.Equals(s.Fqdn, lookupKey, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(s.Name, lookupKey, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matches.Count == 0)
        {
            return false;
        }

        var selected = SelectScopedService(matches, lookupKey, preferredAssembly, preferredProject);
        if (selected is null)
        {
            return false;
        }

        fqdn = selected.Fqdn;
        return true;
    }

    private static bool MatchesServiceScope(ServiceInfo service, string? preferredAssembly, string? preferredProject)
    {
        if (!string.IsNullOrWhiteSpace(preferredProject) &&
            !string.Equals(service.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(preferredAssembly))
        {
            return true;
        }

        if (string.Equals(service.Assembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        var preferredRoot = GetAssemblyRoot(preferredAssembly);
        return !string.IsNullOrWhiteSpace(preferredRoot) &&
            string.Equals(GetAssemblyRoot(service.Assembly), preferredRoot, StringComparison.OrdinalIgnoreCase);
    }

    private bool IsServiceUsageInScope(
        ServiceUsage usage,
        string? preferredAssembly,
        string? preferredProject)
    {
        if (IsFrameworkServiceType(usage.ServiceType) ||
            ContainsRequestProcessor(usage.ServiceType) ||
            usage.ImplementationTypes is { Count: > 0 } impls && impls.Any(IsFrameworkServiceType))
        {
            return true;
        }

        if (_services.TryGetValue(usage.ServiceType, out var directService) &&
            MatchesServiceScope(directService, preferredAssembly, preferredProject))
        {
            return true;
        }

        var implementations = usage.ImplementationTypes is { Count: > 0 }
            ? usage.ImplementationTypes.Where(static t => !string.IsNullOrWhiteSpace(t)).ToList()
            : new List<string>();

        foreach (var implementation in implementations)
        {
            if (_services.TryGetValue(implementation, out var implInfo) &&
                MatchesServiceScope(implInfo, preferredAssembly, preferredProject))
            {
                return true;
            }

            if (TryResolveScopedService(implementation, preferredAssembly, preferredProject, out var scoped) &&
                _services.TryGetValue(scoped, out var scopedInfo) &&
                MatchesServiceScope(scopedInfo, preferredAssembly, preferredProject))
            {
                return true;
            }
        }

        var preferredAssemblyRoot = GetAssemblyRoot(preferredAssembly);
        if (!string.IsNullOrWhiteSpace(preferredAssemblyRoot))
        {
            if (SharesAssemblyRoot(usage.ServiceType, preferredAssemblyRoot))
            {
                return true;
            }

            foreach (var implementation in implementations)
            {
                if (SharesAssemblyRoot(implementation, preferredAssemblyRoot))
                {
                    return true;
                }
            }
        }

        if (implementations.Count == 0)
        {
            return MatchesServiceScopeFallback(usage.ServiceType, preferredAssembly, preferredProject);
        }

        return MatchesServiceScopeFallback(usage.ServiceType, preferredAssembly, preferredProject);
    }

    private bool MatchesServiceScopeFallback(
        string serviceType,
        string? preferredAssembly,
        string? preferredProject)
    {
        if (_services.TryGetValue(serviceType, out var serviceInfo))
        {
            return MatchesServiceScope(serviceInfo, preferredAssembly, preferredProject);
        }

        return string.IsNullOrWhiteSpace(preferredProject) && string.IsNullOrWhiteSpace(preferredAssembly);
    }

    private static readonly HashSet<string> FrameworkServiceSimpleNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "IHttpContextAccessor",
        "HttpContext",
        "HttpRequest",
        "ILogger",
        "IServiceProvider",
        "IServiceScopeFactory",
        "IMemoryCache",
        "IDistributedCache",
        "Log"
    };
    private static bool IsFrameworkServiceType(string? serviceType)
    {
        if (string.IsNullOrWhiteSpace(serviceType))
        {
            return false;
        }

        return serviceType.StartsWith("Microsoft.AspNetCore.", StringComparison.OrdinalIgnoreCase) ||
               serviceType.StartsWith("Microsoft.Extensions.", StringComparison.OrdinalIgnoreCase) ||
               serviceType.StartsWith("System.Net.Http.", StringComparison.OrdinalIgnoreCase) ||
               serviceType.IndexOf("Serilog", StringComparison.OrdinalIgnoreCase) >= 0 ||
               FrameworkServiceSimpleNames.Contains(GetTopLevelSimpleIdentifier(serviceType));
    }

    private static bool SharesAssemblyRoot(string? serviceType, string preferredRoot)
    {
        if (string.IsNullOrWhiteSpace(serviceType))
        {
            return false;
        }

        var candidateRoot = GetTypeAssemblyRoot(serviceType);
        return !string.IsNullOrWhiteSpace(candidateRoot) &&
            string.Equals(candidateRoot, preferredRoot, StringComparison.OrdinalIgnoreCase);
    }

    private ServiceInfo? SelectScopedService(
        IEnumerable<ServiceInfo> candidates,
        string lookupKey,
        string? preferredAssembly,
        string? preferredProject)
    {
        var list = candidates as IList<ServiceInfo> ?? candidates.ToList();
        if (list.Count == 0)
        {
            return null;
        }

        var lookupSimple = GetTopLevelSimpleIdentifier(lookupKey);
        var lookupNamespace = GetTypeNamespace(lookupKey);
        var preferredAssemblyRoot = GetAssemblyRoot(preferredAssembly);

        ServiceInfo? best = null;
        var bestScore = int.MinValue;

        foreach (var candidate in list)
        {
            var score = 0;

            if (string.Equals(candidate.Fqdn, lookupKey, StringComparison.OrdinalIgnoreCase))
            {
                score += 1_000;
            }

            if (!string.IsNullOrWhiteSpace(lookupSimple) &&
                string.Equals(candidate.Name, lookupSimple, StringComparison.OrdinalIgnoreCase))
            {
                score += 250;
            }

            if (!string.IsNullOrWhiteSpace(lookupNamespace) &&
                candidate.Fqdn.StartsWith(lookupNamespace, StringComparison.OrdinalIgnoreCase))
            {
                score += lookupNamespace.Length;
            }

            if (!string.IsNullOrWhiteSpace(preferredProject) &&
                string.Equals(candidate.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
            {
                score += 500;
            }

            if (!string.IsNullOrWhiteSpace(preferredAssembly))
            {
                if (string.Equals(candidate.Assembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
                {
                    score += 400;
                }
                else if (!string.IsNullOrWhiteSpace(preferredAssemblyRoot) &&
                         string.Equals(GetAssemblyRoot(candidate.Assembly), preferredAssemblyRoot, StringComparison.OrdinalIgnoreCase))
                {
                    score += 250;
                }
            }

            if (best is null || score > bestScore ||
                (score == bestScore && CompareServiceInfos(candidate, best) < 0))
            {
                best = candidate;
                bestScore = score;
            }
        }

        return best ?? list
            .OrderBy(info => info.FilePath ?? string.Empty, StringComparer.OrdinalIgnoreCase)
            .ThenBy(info => info.Fqdn, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
    }

    private static int CompareServiceInfos(ServiceInfo left, ServiceInfo right)
    {
        var fileCompare = StringComparer.OrdinalIgnoreCase.Compare(left.FilePath ?? string.Empty, right.FilePath ?? string.Empty);
        if (fileCompare != 0)
        {
            return fileCompare;
        }

        var leftLine = left.Span.StartLine;
        var rightLine = right.Span.StartLine;
        if (leftLine != rightLine)
        {
            return leftLine.CompareTo(rightLine);
        }

        return StringComparer.OrdinalIgnoreCase.Compare(left.Fqdn, right.Fqdn);
    }

    private string? TryResolveProjectionSource(ExpressionSyntax expression, IReadOnlyDictionary<string, string?> parameterTypes, Dictionary<string, string> localVariables, IReadOnlyDictionary<string, FieldDescriptor> fieldLookup, string? preferredAssembly = null, string? preferredProject = null)
    {
        var resolved = TryResolveExpressionType(expression, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
        if (!string.IsNullOrWhiteSpace(resolved))
        {
            return ExtractInnermostGenericType(resolved);
        }

        if (expression is IdentifierNameSyntax identifier)
        {
            var identifierName = identifier.Identifier.Text.TrimStart('_');
            if (fieldLookup.TryGetValue(identifierName, out var descriptor))
            {
                return ExtractInnermostGenericType(descriptor.Type) ?? descriptor.Type;
            }
        }

        if (expression is MemberAccessExpressionSyntax member)
        {
            if (member.Expression is IdentifierNameSyntax rootIdentifier)
            {
                var rootName = rootIdentifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(rootName, out var descriptor) && descriptor.Type.Contains("DbContext", StringComparison.Ordinal))
                {
                    var propertyName = member.Name.Identifier.Text;
                    var entity = _entities.Values.FirstOrDefault(e => e.DbSetProperties.Any(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)));
                    if (entity is not null)
                    {
                        return entity.Fqdn;
                    }
                }
            }

            var nested = TryResolveProjectionSource(member.Expression, parameterTypes, localVariables, fieldLookup, preferredAssembly, preferredProject);
            if (!string.IsNullOrWhiteSpace(nested))
            {
                return nested;
            }
        }

        if (expression is InvocationExpressionSyntax invocation && invocation.Expression is MemberAccessExpressionSyntax invocationAccess)
        {
            var nested = TryResolveProjectionSource(invocationAccess.Expression, parameterTypes, localVariables, fieldLookup, preferredAssembly, preferredProject);
            if (!string.IsNullOrWhiteSpace(nested))
            {
                return nested;
            }
        }

        return null;
    }

    private string? TryResolveExpressionType(
        ExpressionSyntax expression,
        IReadOnlyDictionary<string, string?> parameterTypes,
        Dictionary<string, string> localVariables,
        string? preferredAssembly = null,
        string? preferredProject = null,
        IReadOnlyDictionary<string, FieldDescriptor>? fieldLookup = null)
    {
        if (expression is ParenthesizedExpressionSyntax parenthesized)
        {
            return TryResolveExpressionType(parenthesized.Expression, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
        }

        if (expression is PostfixUnaryExpressionSyntax postfix &&
            postfix.OperatorToken.IsKind(SyntaxKind.ExclamationToken))
        {
            return TryResolveExpressionType(postfix.Operand, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
        }

        if (expression is CastExpressionSyntax cast)
        {
            return QualifyTypeName(cast.Type.ToString(), preferredAssembly, preferredProject);
        }

        if (expression is BinaryExpressionSyntax binary &&
            binary.IsKind(SyntaxKind.AsExpression))
        {
            if (binary.Right is TypeSyntax asType)
            {
                return QualifyTypeName(asType.ToString(), preferredAssembly, preferredProject);
            }

            return TryResolveExpressionType(binary.Right, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
        }

        if (expression is ConditionalAccessExpressionSyntax conditional)
        {
            var whenNotNull = TryResolveExpressionType(conditional.WhenNotNull, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
            if (!string.IsNullOrWhiteSpace(whenNotNull))
            {
                return whenNotNull;
            }

            return TryResolveExpressionType(conditional.Expression, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
        }

        if (expression is IdentifierNameSyntax identifier)
        {
            if (localVariables.TryGetValue(identifier.Identifier.Text, out var localType) && !string.Equals(localType, "var", StringComparison.OrdinalIgnoreCase))
            {
                return QualifyTypeName(localType, preferredAssembly, preferredProject);
            }

            if (parameterTypes.TryGetValue(identifier.Identifier.Text, out var parameterType) && !string.IsNullOrWhiteSpace(parameterType))
            {
                return QualifyTypeName(parameterType, preferredAssembly, preferredProject);
            }

            if (fieldLookup is not null)
            {
                var normalized = identifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(normalized, out var descriptor))
                {
                    return QualifyTypeName(descriptor.Type, preferredAssembly, preferredProject);
                }
            }
        }

        if (expression is InvocationExpressionSyntax invocation)
        {
            var returnType = TryResolveInvocationReturnType(
                invocation,
                parameterTypes,
                localVariables,
                preferredAssembly,
                preferredProject,
                fieldLookup);

            if (!string.IsNullOrWhiteSpace(returnType))
            {
                return QualifyTypeName(returnType!, preferredAssembly, preferredProject);
            }
        }

        if (expression is ObjectCreationExpressionSyntax creation)
        {
            return QualifyTypeName(creation.Type.ToString(), preferredAssembly, preferredProject);
        }

        if (expression is MemberAccessExpressionSyntax memberAccess && fieldLookup is not null)
        {
            if (memberAccess.Expression is IdentifierNameSyntax identifierExpression)
            {
                var normalized = identifierExpression.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(normalized, out var descriptor))
                {
                    return QualifyTypeName(descriptor.Type, preferredAssembly, preferredProject);
                }
            }
        }

        return null;
    }

    private string? TryResolveExpressionType(
        IOperation? operation,
        IReadOnlyDictionary<string, string?> parameterTypes,
        Dictionary<string, string> localVariables,
        string? preferredAssembly = null,
        string? preferredProject = null,
        IReadOnlyDictionary<string, FieldDescriptor>? fieldLookup = null)
    {
        if (operation is null)
        {
            return null;
        }

        if (operation.Type is { } typeSymbol)
        {
            var qualified = QualifySymbol(typeSymbol, preferredAssembly ?? string.Empty, preferredProject ?? string.Empty);
            if (!string.IsNullOrWhiteSpace(qualified))
            {
                return qualified;
            }
        }

        if (operation is ILocalReferenceOperation localReference &&
            localVariables.TryGetValue(localReference.Local.Name, out var localType) &&
            !string.IsNullOrWhiteSpace(localType))
        {
            return QualifyTypeName(localType, preferredAssembly, preferredProject);
        }

        if (operation is IParameterReferenceOperation parameterReference &&
            parameterTypes.TryGetValue(parameterReference.Parameter.Name, out var parameterType) &&
            !string.IsNullOrWhiteSpace(parameterType))
        {
            return QualifyTypeName(parameterType!, preferredAssembly, preferredProject);
        }

        if (operation.Syntax is ExpressionSyntax expressionSyntax)
        {
            return TryResolveExpressionType(expressionSyntax, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
        }

        return null;
    }

    private string? TryResolveInvocationReturnType(
        InvocationExpressionSyntax invocation,
        IReadOnlyDictionary<string, string?> parameterTypes,
        Dictionary<string, string> localVariables,
        string? preferredAssembly,
        string? preferredProject,
        IReadOnlyDictionary<string, FieldDescriptor>? fieldLookup)
    {
        if (invocation.Expression is MemberAccessExpressionSyntax access)
        {
            var methodName = GetMemberName(access.Name);
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return null;
            }

            var factoryType = TryResolveExpressionType(
                access.Expression,
                parameterTypes,
                localVariables,
                preferredAssembly,
                preferredProject,
                fieldLookup);

            if (string.IsNullOrWhiteSpace(factoryType) && access.Expression is IdentifierNameSyntax identifier && fieldLookup is not null)
            {
                var normalized = identifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(normalized, out var descriptor))
                {
                    factoryType = QualifyTypeName(descriptor.Type, preferredAssembly, preferredProject);
                }
            }

            if (!string.IsNullOrWhiteSpace(factoryType))
            {
                var guessed = GuessServiceTypeFromFactory(factoryType, methodName);
                if (!string.IsNullOrWhiteSpace(guessed))
                {
                    return guessed;
                }

                foreach (var key in GetFactoryLookupKeys(factoryType!))
                {
                    if (_interfaceMethodReturnTypes.TryGetValue(key, out var methodMap) &&
                        methodMap.TryGetValue(methodName!, out var mappedReturn) &&
                        !string.IsNullOrWhiteSpace(mappedReturn))
                    {
                        return mappedReturn;
                    }
                }
            }

            var fallbackGuess = GuessServiceTypeFromFactory(null, methodName);
            if (!string.IsNullOrWhiteSpace(fallbackGuess))
            {
                var qualifiedFallback = QualifyTypeName(fallbackGuess, preferredAssembly, preferredProject);
                if (!string.IsNullOrWhiteSpace(qualifiedFallback) &&
                    !string.Equals(qualifiedFallback, fallbackGuess, StringComparison.Ordinal))
                {
                    return qualifiedFallback;
                }
            }

            return null;
        }

        if (invocation.Expression is IdentifierNameSyntax identifierName)
        {
            if (localVariables.TryGetValue(identifierName.Identifier.Text, out var localType) && !string.IsNullOrWhiteSpace(localType))
            {
                return localType;
            }

            if (parameterTypes.TryGetValue(identifierName.Identifier.Text, out var parameterType) && !string.IsNullOrWhiteSpace(parameterType))
            {
                return parameterType;
            }
        }

        return null;
    }
    private string QualifyTypeName(string typeName, string? preferredAssembly = null, string? preferredProject = null)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return typeName;
        }

        if (_requests.ContainsKey(typeName) || _handlers.ContainsKey(typeName) || _notifications.ContainsKey(typeName))
        {
            return typeName;
        }

        var simple = GetSimpleIdentifier(typeName);

        var requestMatches = _requests.Values
            .Where(r => r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (requestMatches.Count > 1)
        {
            if (!string.IsNullOrWhiteSpace(preferredAssembly))
            {
                var assemblyMatches = requestMatches
                    .Where(r => string.Equals(r.Assembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (assemblyMatches.Count == 1)
                {
                    return assemblyMatches[0].Fqdn;
                }

                if (assemblyMatches.Count > 1 && !string.IsNullOrWhiteSpace(preferredProject))
                {
                    var projectMatches = assemblyMatches
                        .Where(r => string.Equals(r.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    if (projectMatches.Count == 1)
                    {
                        return projectMatches[0].Fqdn;
                    }
                }

                if (assemblyMatches.Count == 0)
                {
                    var preferredRoot = GetAssemblyRoot(preferredAssembly);
                    if (!string.IsNullOrWhiteSpace(preferredRoot))
                    {
                        var rootMatches = requestMatches
                            .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), preferredRoot, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        if (rootMatches.Count == 1)
                        {
                            return rootMatches[0].Fqdn;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(preferredProject))
            {
                var projectMatches = requestMatches
                    .Where(r => string.Equals(r.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (projectMatches.Count == 1)
                {
                    return projectMatches[0].Fqdn;
                }
            }

            if (!string.IsNullOrWhiteSpace(preferredAssembly))
            {
                var preferredRoot = GetAssemblyRoot(preferredAssembly);
                if (!string.IsNullOrWhiteSpace(preferredRoot))
                {
                    var rootMatches = requestMatches
                        .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), preferredRoot, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    if (rootMatches.Count == 1)
                    {
                        return rootMatches[0].Fqdn;
                    }
                }
            }
        }
        if (requestMatches.Count == 1)
        {
            return requestMatches[0].Fqdn;
        }

        var handlerMatches = _handlers.Values
            .Where(h => h.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (handlerMatches.Count > 1 && !string.IsNullOrWhiteSpace(preferredAssembly))
        {
            var assemblyMatches = handlerMatches
                .Where(h => string.Equals(h.Assembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (assemblyMatches.Count == 1)
            {
                return assemblyMatches[0].Fqdn;
            }

            if (assemblyMatches.Count > 1 && !string.IsNullOrWhiteSpace(preferredProject))
            {
                var projectFiltered = assemblyMatches
                    .Where(h => string.Equals(h.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (projectFiltered.Count == 1)
                {
                    return projectFiltered[0].Fqdn;
                }
            }

            if (assemblyMatches.Count == 0)
            {
                var preferredRoot = GetAssemblyRoot(preferredAssembly);
                if (!string.IsNullOrWhiteSpace(preferredRoot))
                {
                    var rootMatches = handlerMatches
                        .Where(h => string.Equals(GetAssemblyRoot(h.Assembly), preferredRoot, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    if (rootMatches.Count == 1)
                    {
                        return rootMatches[0].Fqdn;
                    }
                }
            }
        }
        if (handlerMatches.Count == 1)
        {
            return handlerMatches[0].Fqdn;
        }

        if (!string.IsNullOrWhiteSpace(preferredProject))
        {
            var projectMatches = handlerMatches
                .Where(h => string.Equals(h.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (projectMatches.Count == 1)
            {
                return projectMatches[0].Fqdn;
            }
        }

        return typeName;
    }

    private static string? ExtractInnermostGenericType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var current = typeName;
        while (true)
        {
            var generic = ExtractGenericArgument(current);
            if (string.IsNullOrWhiteSpace(generic))
            {
                return current;
            }

            var separator = generic.IndexOf(',');
            if (separator >= 0)
            {
                generic = generic[..separator];
            }

            current = generic;
        }
    }

    private static IReadOnlyList<string> SplitGenericArguments(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return Array.Empty<string>();
        }

        var start = typeName.IndexOf('<');
        var end = typeName.LastIndexOf('>');
        if (start < 0 || end <= start)
        {
            return Array.Empty<string>();
        }

        var inner = typeName.Substring(start + 1, end - start - 1);
        var arguments = new List<string>();
        var depth = 0;
        var current = new List<char>();
        foreach (var ch in inner)
        {
            if (ch == '<')
            {
                depth++;
                current.Add(ch);
                continue;
            }

            if (ch == '>')
            {
                depth--;
                current.Add(ch);
                continue;
            }

            if (ch == ',' && depth == 0)
            {
                var value = new string(current.ToArray()).Trim();
                if (value.Length > 0)
                {
                    arguments.Add(value);
                }
                current.Clear();
                continue;
            }

            current.Add(ch);
        }

        var last = new string(current.ToArray()).Trim();
        if (last.Length > 0)
        {
            arguments.Add(last);
        }

        return arguments;
    }

    private static string TrimGlobalAlias(string typeName)
        => typeName.StartsWith("global::", StringComparison.Ordinal)
            ? typeName["global::".Length..]
            : typeName;

    private static string? GetInvocationIdentifier(SyntaxNode expression)
    {
        return expression switch
        {
            IdentifierNameSyntax identifier => identifier.Identifier.Text,
            GenericNameSyntax generic => generic.Identifier.Text,
            MemberAccessExpressionSyntax member => GetMemberName(member.Name),
            MemberBindingExpressionSyntax binding => GetMemberName(binding.Name),
            _ => null
        };
    }

    private static string GetSimpleIdentifier(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return typeName;
        }

        var trimmed = TrimGlobalAlias(typeName.Trim());

        var genericArguments = SplitGenericArguments(trimmed);
        if (genericArguments.Count > 0)
        {
            for (var i = genericArguments.Count - 1; i >= 0; i--)
            {
                var simpleCandidate = GetSimpleIdentifier(genericArguments[i]);
                if (!string.IsNullOrWhiteSpace(simpleCandidate))
                {
                    return simpleCandidate;
                }
            }
        }

        return NormalizeTopLevelIdentifier(trimmed);
    }

    private static int GetGenericArity(string openType)
    {
        if (string.IsNullOrWhiteSpace(openType))
        {
            return 0;
        }

        var start = openType.IndexOf('<');
        var end = openType.LastIndexOf('>');
        if (start < 0 || end <= start)
        {
            return 0;
        }

        var inner = openType.Substring(start + 1, end - start - 1);
        if (string.IsNullOrWhiteSpace(inner))
        {
            return 1;
        }

        var arity = 1;
        for (var i = 0; i < inner.Length; i++)
        {
            if (inner[i] == ',')
            {
                arity++;
            }
        }

        return arity;
    }

    private static string GetTopLevelSimpleIdentifier(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return typeName;
        }

        var trimmed = TrimGlobalAlias(typeName.Trim());

        return NormalizeTopLevelIdentifier(trimmed);
    }

    private static string NormalizeTopLevelIdentifier(string typeName)
    {
        var withoutGenerics = GetTypeNameWithoutGenerics(typeName);

        if (withoutGenerics.EndsWith("?", StringComparison.Ordinal))
        {
            withoutGenerics = withoutGenerics[..^1];
        }

        while (withoutGenerics.EndsWith("[]", StringComparison.Ordinal))
        {
            withoutGenerics = withoutGenerics[..^2];
        }

        var lastDot = withoutGenerics.LastIndexOf('.');
        var candidate = (lastDot >= 0 ? withoutGenerics[(lastDot + 1)..] : withoutGenerics).Trim();
        if (string.IsNullOrEmpty(candidate))
        {
            return candidate;
        }

        if (candidate.IndexOfAny(new[] { ' ', '(', ')', '{', '}', '[', ']', ',' }) >= 0)
        {
            return candidate;
        }

        return ExtractIdentifierToken(candidate);
    }

    private static string ExtractIdentifierToken(string candidate)
    {
        var span = candidate.AsSpan();
        string? lastToken = null;
        var tokenStart = -1;
        for (var i = 0; i < span.Length; i++)
        {
            var ch = span[i];
            if (char.IsLetterOrDigit(ch) || ch == '_')
            {
                if (tokenStart < 0)
                {
                    tokenStart = i;
                }
            }
            else if (tokenStart >= 0)
            {
                lastToken = span.Slice(tokenStart, i - tokenStart).ToString();
                tokenStart = -1;
            }
        }

        if (tokenStart >= 0)
        {
            lastToken = span.Slice(tokenStart).ToString();
        }

        return string.IsNullOrWhiteSpace(lastToken) ? candidate : lastToken!;
    }

    private static int FindLastDotOutsideGenerics(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return -1;
        }

        var trimmed = TrimGlobalAlias(typeName.Trim());
        var depth = 0;
        var lastDot = -1;
        for (var i = 0; i < trimmed.Length; i++)
        {
            var ch = trimmed[i];
            switch (ch)
            {
                case '<':
                    depth++;
                    break;
                case '>':
                    if (depth > 0)
                    {
                        depth--;
                    }
                    break;
                case ':':
                    if (i + 1 < trimmed.Length && trimmed[i + 1] == ':')
                    {
                        i++;
                    }
                    break;
                case '.':
                    if (depth == 0)
                    {
                        lastDot = i;
                    }
                    break;
            }
        }

        return lastDot;
    }

    private static int FindFirstDotOutsideGenerics(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return -1;
        }

        var trimmed = TrimGlobalAlias(typeName.Trim());
        var depth = 0;
        for (var i = 0; i < trimmed.Length; i++)
        {
            var ch = trimmed[i];
            switch (ch)
            {
                case '<':
                    depth++;
                    break;
                case '>':
                    if (depth > 0)
                    {
                        depth--;
                    }
                    break;
                case ':':
                    if (i + 1 < trimmed.Length && trimmed[i + 1] == ':')
                    {
                        i++;
                    }
                    break;
                case '.':
                    if (depth == 0)
                    {
                        return i;
                    }
                    break;
            }
        }

        return -1;
    }

    private static string GetTypeNamespace(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return string.Empty;
        }

        var lastDot = FindLastDotOutsideGenerics(typeName);
        return lastDot <= 0 ? string.Empty : TrimGlobalAlias(typeName.Trim())[..lastDot];
    }

    private static string GetTypeAssemblyRoot(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return string.Empty;
        }

        var trimmed = TrimGlobalAlias(typeName.Trim());
        var baseType = GetTypeNameWithoutGenerics(trimmed);
        var firstDot = FindFirstDotOutsideGenerics(baseType);
        if (firstDot < 0)
        {
            return string.Empty;
        }

        if (firstDot == 0)
        {
            return string.Empty;
        }

        return baseType[..firstDot];
    }

    private static bool IsGenericPlaceholder(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return true;
        }

        var simple = GetTopLevelSimpleIdentifier(typeName);
        if (string.IsNullOrWhiteSpace(simple))
        {
            return true;
        }

        if (!simple.Contains('.', StringComparison.Ordinal))
        {
            if (simple.Length == 1)
            {
                return true;
            }

            if (simple.All(char.IsUpper))
            {
                return true;
            }

            if (simple.Length > 1 && simple[0] == 'T' && char.IsUpper(simple[1]))
            {
                var remainder = simple.AsSpan(2);
                if (remainder.IsEmpty || remainder.ToString().All(char.IsLetter))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool HasConcreteGenericArguments(string typeName)
    {
        var arguments = SplitGenericArguments(typeName);
        if (arguments.Count == 0)
        {
            return false;
        }

        foreach (var argument in arguments)
        {
            if (!IsGenericPlaceholder(argument))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsOptionsDeclaration(TypeDeclarationSyntax declaration)
    {
        if (declaration.Identifier.Text.EndsWith("Options", StringComparison.Ordinal))
        {
            return true;
        }

        return declaration.Members.OfType<FieldDeclarationSyntax>().Any(field =>
            field.Modifiers.Any(m => m.IsKind(SyntaxKind.ConstKeyword)) &&
            field.Declaration.Type.ToString().Equals("string", StringComparison.OrdinalIgnoreCase) &&
            field.Declaration.Variables.Any(variable =>
                variable.Identifier.Text.Equals("SectionName", StringComparison.OrdinalIgnoreCase)));
    }

private static int LongestCommonPrefixLength(string a, string b)
{
    if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
    {
        return 0;
        }

        var max = Math.Min(a.Length, b.Length);
        var i = 0;
        for (; i < max; i++)
        {
            if (a[i] != b[i]) break;
    }
    return i;
}

private static string? GetNamespaceRoot(string? typeName)
{
    if (string.IsNullOrWhiteSpace(typeName))
    {
        return null;
    }

    var ns = GetTypeNamespace(typeName);
    if (string.IsNullOrWhiteSpace(ns))
    {
        return null;
    }

    var separatorIndex = ns.IndexOf('.');
    return separatorIndex >= 0 ? ns[..separatorIndex] : ns;
}

private static bool NamespaceRootMatches(string candidateType, string referenceType)
{
    var referenceRoot = GetNamespaceRoot(referenceType);
    if (string.IsNullOrWhiteSpace(referenceRoot))
    {
        return true;
    }

    var candidateRoot = GetNamespaceRoot(candidateType);
    if (string.IsNullOrWhiteSpace(candidateRoot))
    {
        return false;
    }

    return string.Equals(candidateRoot, referenceRoot, StringComparison.OrdinalIgnoreCase);
}

    private static string GetProjectRoot(string? project)
    {
        if (string.IsNullOrWhiteSpace(project))
        {
            return string.Empty;
        }

        var normalized = project.Replace('\\', '/');
        var separatorIndex = normalized.IndexOf('/');
        return separatorIndex > 0 ? normalized[..separatorIndex] : normalized;
    }

    private static bool IsTestProject(string? project)
    {
        if (string.IsNullOrWhiteSpace(project))
        {
            return false;
        }

        return project.IndexOf(".Tests", StringComparison.OrdinalIgnoreCase) >= 0 ||
               project.IndexOf(".Test", StringComparison.OrdinalIgnoreCase) >= 0 ||
               project.IndexOf(".Acceptance", StringComparison.OrdinalIgnoreCase) >= 0 ||
               project.IndexOf(".Integration", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static bool LooksLikeTestImplementation(string? fqdn)
    {
        if (string.IsNullOrWhiteSpace(fqdn))
        {
            return false;
        }

        return fqdn.IndexOf(".Tests.", StringComparison.OrdinalIgnoreCase) >= 0 ||
               fqdn.EndsWith(".Tests", StringComparison.OrdinalIgnoreCase) ||
               fqdn.IndexOf(".Test.", StringComparison.OrdinalIgnoreCase) >= 0 ||
               fqdn.IndexOf(".Acceptance", StringComparison.OrdinalIgnoreCase) >= 0 ||
               fqdn.IndexOf(".Integration", StringComparison.OrdinalIgnoreCase) >= 0 ||
               fqdn.IndexOf("Mock", StringComparison.OrdinalIgnoreCase) >= 0 ||
               fqdn.IndexOf("Fake", StringComparison.OrdinalIgnoreCase) >= 0 ||
               fqdn.IndexOf("Stub", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static string GetAssemblyRoot(string? assembly)
    {
        if (string.IsNullOrWhiteSpace(assembly))
        {
            return string.Empty;
        }

        var separatorIndex = assembly.IndexOf('.');
        return separatorIndex > 0 ? assembly[..separatorIndex] : assembly;
    }

    internal static bool IsLoggerType(string? typeName)
        => !string.IsNullOrWhiteSpace(typeName) &&
           (typeName.Contains("ILogger", StringComparison.Ordinal) ||
            typeName.Contains("Serilog", StringComparison.OrdinalIgnoreCase) ||
            typeName.Contains(".Logger", StringComparison.OrdinalIgnoreCase));

    private static bool IsMetricsType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        return typeName.Contains("System.Diagnostics.Metrics", StringComparison.Ordinal)
            || typeName.Contains("Microsoft.Extensions.Diagnostics.Metrics", StringComparison.Ordinal)
            || typeName.Contains("Prometheus", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("OpenTelemetry", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("IMeter", StringComparison.Ordinal)
            || typeName.Contains("IMetrics", StringComparison.Ordinal)
            || typeName.Contains("MeterProvider", StringComparison.Ordinal)
            || typeName.Contains("MeterFactory", StringComparison.Ordinal);
    }

    private static bool IsTelemetryType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        return typeName.Contains("Telemetry", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("ActivitySource", StringComparison.Ordinal)
            || typeName.Contains("DiagnosticListener", StringComparison.Ordinal)
            || typeName.Contains("Tracer", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("Tracing", StringComparison.OrdinalIgnoreCase);
    }

    private static string? TryExtractLogLevel(string? methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        if (methodName.StartsWith("Log", StringComparison.OrdinalIgnoreCase) && methodName.Length > 3)
        {
            var level = methodName[3..];
            if (!string.IsNullOrWhiteSpace(level))
            {
                return level;
            }
        }

        return null;
    }

    private static string? NormalizeTypeToken(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return typeName;
        }

        var trimmed = TrimGlobalAlias(typeName.Trim());
        var genericArguments = SplitGenericArguments(trimmed);
        if (genericArguments.Count == 0)
        {
            return NormalizeTopLevelIdentifier(trimmed);
        }

        var baseTypeEnd = trimmed.IndexOf('<');
        var baseType = baseTypeEnd >= 0 ? trimmed[..baseTypeEnd] : trimmed;
        var builder = new StringBuilder();
        builder.Append(NormalizeTopLevelIdentifier(baseType));
        builder.Append('<');

        for (var i = 0; i < genericArguments.Count; i++)
        {
            if (i > 0)
            {
                builder.Append(',');
            }

            var normalizedArgument = NormalizeTypeToken(genericArguments[i]) ?? NormalizeTopLevelIdentifier(genericArguments[i]);
            builder.Append(string.IsNullOrWhiteSpace(normalizedArgument)
                ? genericArguments[i].Trim()
                : normalizedArgument);
        }

        builder.Append('>');
        return builder.ToString();
    }

    private IReadOnlyList<string> DeriveInterfaceLookupKeys(string? interfaceType)
    {
        if (string.IsNullOrWhiteSpace(interfaceType))
        {
            return Array.Empty<string>();
        }

        var keys = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        void Add(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var trimmedKey = value.Trim();
            if (seen.Add(trimmedKey))
            {
                keys.Add(trimmedKey);
            }
        }

        Add(interfaceType);

        var trimmed = TrimGlobalAlias(interfaceType.Trim());
        if (!string.Equals(trimmed, interfaceType, StringComparison.Ordinal))
        {
            Add(trimmed);
        }

        Add(NormalizeTypeToken(interfaceType));
        Add(NormalizeTypeToken(trimmed));

        var qualified = QualifyTypeName(trimmed);
        Add(qualified);
        Add(NormalizeTypeToken(qualified));

        return keys.Count == 0 ? Array.Empty<string>() : keys;
    }

    private static string? BuildAuthLabel(bool allowsAnonymous, IReadOnlyCollection<EndpointAuthorization> authorizations)
    {
        if (allowsAnonymous)
        {
            return "anonymous";
        }

        if (authorizations is not { Count: > 0 })
        {
            return null;
        }

        foreach (var auth in authorizations)
        {
            if (!string.IsNullOrWhiteSpace(auth.Policy))
            {
                return auth.Policy;
            }

            if (!string.IsNullOrWhiteSpace(auth.Roles))
            {
                return auth.Roles;
            }

            if (!string.IsNullOrWhiteSpace(auth.AuthenticationSchemes))
            {
                return auth.AuthenticationSchemes;
            }
        }

        return "user";
    }

    private static bool IsGuardInvocation(MemberAccessExpressionSyntax access)
    {
        var expressionText = access.Expression.ToString();
        if (!string.IsNullOrWhiteSpace(expressionText) && expressionText.Contains("Guard", StringComparison.Ordinal))
        {
            return true;
        }

        var method = access.Name.Identifier.Text;
        if (string.IsNullOrWhiteSpace(method))
        {
            return false;
        }

        return method.StartsWith("Ensure", StringComparison.OrdinalIgnoreCase) ||
               method.StartsWith("Validate", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsStorageService(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var simple = GetTypeNameWithoutGenerics(typeName);
        return simple.Contains("Storage", StringComparison.OrdinalIgnoreCase) ||
               simple.Contains("Blob", StringComparison.OrdinalIgnoreCase) ||
               simple.Contains("FileStore", StringComparison.OrdinalIgnoreCase) ||
               simple.Contains("DocumentStore", StringComparison.OrdinalIgnoreCase);
    }
}
