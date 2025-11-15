using GraphKit.Analyzers;
using Microsoft.CodeAnalysis;

namespace GraphKit.Classification
{
    /// <summary>
    /// Coarse-grained type classification based on Roslyn symbols and existing analyzer predicates.
    /// This is intentionally conservative and reuses existing helpers instead of re-inventing heuristics.
    /// </summary>
    public readonly struct TypeClassification
    {
        public bool IsRepository { get; init; }
        public bool IsDbContext { get; init; }
        public bool IsHttpClient { get; init; }
        public bool IsCache { get; init; }
        public bool IsLogger { get; init; }
        public bool IsDomainType { get; init; }
        public bool IsInfrastructureService { get; init; }
    }

    public sealed class TypeClassifier
    {
        /// <summary>
        /// Classify a type symbol into coarse semantic buckets.
        /// </summary>
        public TypeClassification Classify(ITypeSymbol? symbol)
        {
            if (symbol is null)
            {
                return default;
            }

            var display = symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);

            var isRepo = AnalysisPredicates.IsRepositoryType(symbol);
            var isEf = AnalysisPredicates.IsEntityFrameworkType(symbol);
            var isHttpClient = AnalysisPredicates.IsHttpClientType(symbol);
            var isCache = AnalysisPredicates.IsCachingType(symbol);
            var isLogger = ProjectAnalyzer.IsLoggerType(display);

            // Domain types: anything in a .Domain(.Model) namespace is a strong hint.
            var isDomain = display.Contains(".Domain.", System.StringComparison.OrdinalIgnoreCase) ||
                           display.Contains(".DomainModel.", System.StringComparison.OrdinalIgnoreCase);

            // Infrastructure services: loggers, caches, HTTP context, tenant/identity helpers are typically infra.
            var isInfra = isLogger || isCache ||
                          display.Contains("IHttpContextAccessor", System.StringComparison.OrdinalIgnoreCase) ||
                          display.Contains("TenantService", System.StringComparison.OrdinalIgnoreCase) ||
                          display.Contains("TenantIdentificationService", System.StringComparison.OrdinalIgnoreCase) ||
                          display.Contains("RequestInfoService", System.StringComparison.OrdinalIgnoreCase);

            return new TypeClassification
            {
                IsRepository = isRepo,
                IsDbContext = isEf && !isRepo,
                IsHttpClient = isHttpClient,
                IsCache = isCache,
                IsLogger = isLogger,
                IsDomainType = isDomain,
                IsInfrastructureService = isInfra
            };
        }
    }
}

