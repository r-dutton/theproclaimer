[web] GET /api/sources/export/types  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetSourceTypes)  [L54–L59] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceTypesQuery [L58]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.SourceTypes.GetSourceTypesQueryHandler.Handle [L33–L62]
      └─ maps_to SourceTypeLightDto [L58]
      └─ uses_service IControlledRepository<SourceType>
        └─ method ReadQuery [L46]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L59]
          └─ ... (no implementation details available)

