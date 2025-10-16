[web] GET /api/sources/types  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetSourceTypes)  [L108–L112] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceTypesQuery -> GetSourceTypesQueryHandler [L111]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.SourceTypes.GetSourceTypesQueryHandler.Handle [L33–L62]
      └─ calls SourceTypesRepository.ReadQuery [L46]
      └─ maps_to SourceTypeLightDto [L58]
  └─ impact_summary
    └─ requests 1
      └─ GetSourceTypesQuery
    └─ handlers 1
      └─ GetSourceTypesQueryHandler
    └─ mappings 1
      └─ SourceTypeLightDto

