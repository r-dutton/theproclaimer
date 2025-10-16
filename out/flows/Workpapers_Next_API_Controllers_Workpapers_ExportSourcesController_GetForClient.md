[web] GET /api/sources/export/for-client/{clientId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetForClient)  [L44–L48] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourcesForClientQuery -> GetExportSourcesForClientQueryHandler [L47]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourcesForClientQueryHandler.Handle [L29–L47]
      └─ maps_to ExportSourceLightDto [L42]
        └─ automapper.registration WorkpapersMappingProfile (ExportSource->ExportSourceLightDto) [L601]
      └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
        └─ method ReadQuery [L42]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetExportSourcesForClientQuery
    └─ handlers 1
      └─ GetExportSourcesForClientQueryHandler
    └─ mappings 1
      └─ ExportSourceLightDto

