[web] GET /api/templates/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.TemplatesController.DownloadTemplate)  [L72–L77]
  └─ sends_request DownloadTemplateQuery [L75]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.DownloadTemplateQueryHandler.Handle [L24–L60]
      └─ uses_service IControlledRepository<Template>
        └─ method ReadQuery [L45]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L57]
      └─ uses_service StorageService
        └─ method CreateDownloadUrl [L58]

