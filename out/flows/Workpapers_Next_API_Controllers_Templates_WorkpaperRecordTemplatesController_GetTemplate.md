[web] GET /api/workpaper-record-templates/{id:Guid}/template  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetTemplate)  [L78–L82]
  └─ sends_request GetWorkpaperRecordTemplateToInsertQuery [L81]
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

