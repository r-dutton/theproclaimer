[web] GET /api/sources/types  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetSourceTypes)  [L108–L112] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceTypesQuery [L111]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.SourceTypes.GetSourceTypesQueryHandler.Handle [L33–L62]
      └─ maps_to SourceTypeLightDto [L58]
      └─ uses_service IControlledRepository<SourceType>
        └─ method ReadQuery [L46]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L59]
          └─ ... (no implementation details available)

