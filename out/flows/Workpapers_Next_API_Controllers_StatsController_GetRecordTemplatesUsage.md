[web] GET /api/stats/usage/template  (Workpapers.Next.API.Controllers.StatsController.GetRecordTemplatesUsage)  [L87–L99] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetRecordTemplatesUsageQuery [L96]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetRecordTemplatesUsageQueryHandler.Handle [L65–L237]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L133]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateUltraLightDto) [L216]
      └─ uses_client KeenClient [L154]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L172]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L178]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L133]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L135]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L154]
          └─ ... (no implementation details available)
      └─ uses_service KeenQueryBuilder
        └─ method Build [L155]
          └─ ... (no implementation details available)
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TemplateUsage-period{*}-firmid{*}) [L102]

