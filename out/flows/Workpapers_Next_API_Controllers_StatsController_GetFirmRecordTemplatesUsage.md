[web] GET /api/stats/usage/{firmId:guid}/template  (Workpapers.Next.API.Controllers.StatsController.GetFirmRecordTemplatesUsage)  [L71–L85] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetRecordTemplatesUsageQuery [L82]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetRecordTemplatesUsageQueryHandler.Handle [L65–L237]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L133]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateUltraLightDto) [L216]
      └─ uses_client KeenClient [L154]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L172]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L178]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L133]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L135]
      └─ uses_service KeenClient
        └─ method QueryAsync [L154]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L155]
      └─ uses_cache IMemoryCache [L102]
        └─ method GetOrCreateAsync [read] (key=TemplateUsage-period{*}-firmid{*}) [L102]

