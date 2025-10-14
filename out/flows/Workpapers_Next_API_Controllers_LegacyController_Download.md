[web] GET /api/files/download  (Workpapers.Next.API.Controllers.LegacyController.Download)  [L69–L87]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L84]
  └─ uses_service UnitOfWork
    └─ method Table [L72]
  └─ uses_service UserService
    └─ method GetUser [L76]
  └─ sends_request GetTemplateForDateTimeQuery [L76]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L23]

