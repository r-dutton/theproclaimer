[web] GET /api/worksheets/download  (Workpapers.Next.API.Controllers.WorksheetsController.Download)  [L83–L99]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L95]
  └─ uses_service UserService
    └─ method GetUser [L86]
  └─ sends_request GetTemplateForDateTimeQuery [L86]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L23]

