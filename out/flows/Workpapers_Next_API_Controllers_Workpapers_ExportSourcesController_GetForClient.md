[web] GET /api/sources/export/for-client/{clientId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetForClient)  [L44–L48] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourcesForClientQuery [L47]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourcesForClientQueryHandler.Handle [L29–L47]
      └─ maps_to ExportSourceLightDto [L42]
        └─ automapper.registration WorkpapersMappingProfile (ExportSource->ExportSourceLightDto) [L601]
      └─ uses_service IControlledRepository<ExportSource>
        └─ method ReadQuery [L42]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L44]
          └─ ... (no implementation details available)

