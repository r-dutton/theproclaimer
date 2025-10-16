[web] GET /api/stats/firm-preferences  (Workpapers.Next.API.Controllers.StatsController.FirmPreferences)  [L252–L263] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=FirmPreferences) [L255]
  └─ sends_request GetFirmPreferencesStatsQuery -> GetFirmPreferencesStatsQueryHandler [L259]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetFirmPreferencesStatsQueryHandler.Handle [L39–L213]
      └─ uses_service List<FirmPreferenceStat>
        └─ method Add [L174]
          └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.Add [L60-L77]
            └─ calls PublishedReportBatchRepository.ReadQuery [L66]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method ProcessAsync [L70]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
                  └─ constructs RequestProcessorWrapper<CanIAccessFileQuery,Unit>
                  └─ resolves IPipelineBehavior<CanIAccessFileQuery,Unit> chain
                  └─ invokes IAsyncRequestHandler<CanIAccessFileQuery,Unit>.Handle
                  └─ dispatches CanIAccessFileQuery [L70]
            └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
              └─ method ReadQuery [L66]
                └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
            └─ query PublishedReportBatch [L66]
            └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70]
            └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
      └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
        └─ method ReadQuery [L136]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
      └─ uses_service UnitOfWork
        └─ method Table [L68]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PublishedReportBatch writes=0 reads=1
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ GetFirmPreferencesStatsQuery
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ GetFirmPreferencesStatsQueryHandler
    └─ caches 1
      └─ IMemoryCache

