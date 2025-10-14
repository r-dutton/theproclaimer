[web] POST /api/sources/export/  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.Create)  [L79–L86] [auth=AuthorizationPolicies.User]
  └─ sends_request CreateExportSourceCommand [L84]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.ExportSources.CreateExportSourceCommandHandler.Handle [L28–L76]
      └─ uses_service IControlledRepository<ExportSource>
        └─ method Add [L45]
      └─ uses_service IControlledRepository<SourceType>
        └─ method ReadQuery [L65]

