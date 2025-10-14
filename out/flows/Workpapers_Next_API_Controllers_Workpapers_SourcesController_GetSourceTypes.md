[web] GET /api/sources/types  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetSourceTypes)  [L108–L112] [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceTypesQuery [L111]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.SourceTypes.GetSourceTypesQueryHandler.Handle [L33–L62]
      └─ maps_to SourceTypeLightDto [L58]
      └─ uses_service IControlledRepository<SourceType>
        └─ method ReadQuery [L46]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L59]

