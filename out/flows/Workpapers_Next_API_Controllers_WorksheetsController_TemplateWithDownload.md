[web] GET /api/worksheets/templatewithdownload  (Workpapers.Next.API.Controllers.WorksheetsController.TemplateWithDownload)  [L104–L122]
  └─ maps_to WorkpaperRecordTemplateV2Dto [L118]
    └─ converts_to LegacyDocumentDto [L343]
    └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L119]
  └─ uses_service UnitOfWork
    └─ method Table [L118]
  └─ uses_service UserService
    └─ method GetUser [L107]
  └─ sends_request GetTemplateForDateTimeQuery [L107]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L23]

