[web] GET /api/workpaper-record-templates/{id:Guid}/download-template  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.DownloadTemplate)  [L84–L89]
  └─ sends_request DownloadTemplateQuery [L88]
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
  └─ sends_request GetWorkpaperRecordTemplateToInsertQuery [L87]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateToInsertQueryHandler.Handle [L36–L71]
      └─ maps_to TemplateLightDto [L69]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L57]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L62]
      └─ uses_service IMapper
        └─ method Map [L69]

