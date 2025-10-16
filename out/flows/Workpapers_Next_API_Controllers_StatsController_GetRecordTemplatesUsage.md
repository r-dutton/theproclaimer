[web] GET /api/stats/usage/template  (Workpapers.Next.API.Controllers.StatsController.GetRecordTemplatesUsage)  [L87–L99] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetRecordTemplatesUsageQuery -> GetRecordTemplatesUsageQueryHandler [L96]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetRecordTemplatesUsageQueryHandler.Handle [L65–L237]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L133]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateUltraLightDto) [L216]
      └─ uses_client KeenClient [L154]
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method ReadQuery [L178]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L172]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service KeenQueryBuilder
        └─ method Build [L155]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L154]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L133]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TemplateUsage-period{*}-firmid{*}) [L102]
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ requests 1
      └─ GetRecordTemplatesUsageQuery
    └─ handlers 1
      └─ GetRecordTemplatesUsageQueryHandler
    └─ mappings 1
      └─ WorkpaperRecordTemplateUltraLightDto

