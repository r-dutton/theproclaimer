[web] GET /api/sources/export/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.Get)  [L34–L38] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourceQuery [L37]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceQueryHandler.Handle [L26–L43]
      └─ maps_to ExportSourceDto [L39]
        └─ automapper.registration WorkpapersMappingProfile (ExportSource->ExportSourceDto) [L602]
      └─ uses_service IControlledRepository<ExportSource>
        └─ method ReadQuery [L39]
          └─ ... (no implementation details available)

