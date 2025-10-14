[web] GET /api/workpaper-record-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Get)  [L72–L76]
  └─ sends_request GetWorkpaperRecordTemplateQuery [L75]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateQueryHandler.Handle [L22–L144]
      └─ maps_to SectionLightDto [L82]
      └─ maps_to WorkpaperRecordTemplateAsFirmDto [L49]
      └─ maps_to WorkpaperRecordTemplateAsSuperDto [L48]
      └─ maps_to WorkpaperRecordTemplateLightWithTemplatesDto [L73]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L56]
      └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
        └─ method ReadQuery [L56]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L48]
      └─ uses_service UnitOfWork
        └─ method Table [L51]
      └─ uses_service UserService
        └─ method IsSuperUser [L47]

