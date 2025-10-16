[web] GET /api/sources/export/types  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetSourceTypes)  [L54–L59] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceTypesQuery -> GetSourceTypesQueryHandler [L58]
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

