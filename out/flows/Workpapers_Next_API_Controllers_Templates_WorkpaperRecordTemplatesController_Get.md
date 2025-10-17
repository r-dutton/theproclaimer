[web] GET /api/workpaper-record-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Get)  [L72–L76] status=200
  └─ sends_request GetWorkpaperRecordTemplateQuery -> GetWorkpaperRecordTemplateQueryHandler [L75]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateQueryHandler.Handle [L22–L144]
      └─ maps_to SectionLightDto [L82]
      └─ maps_to WorkpaperRecordTemplateLightWithTemplatesDto [L73]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L56]
      └─ maps_to WorkpaperRecordTemplateAsFirmDto [L49]
      └─ maps_to WorkpaperRecordTemplateAsSuperDto [L48]
      └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping> (Scoped (inferred))
        └─ method ReadQuery [L56]
          └─ implementation Workpapers.Next.Data.Repository.Templates.ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery
      └─ uses_service UnitOfWork
        └─ method Table [L51]
          └─ implementation UnitOfWork.Table
      └─ uses_service UserService
        └─ method IsSuperUser [L47]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ requests 1
      └─ GetWorkpaperRecordTemplateQuery
    └─ handlers 1
      └─ GetWorkpaperRecordTemplateQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 5
      └─ SectionLightDto
      └─ WorkpaperRecordTemplateAsFirmDto
      └─ WorkpaperRecordTemplateAsSuperDto
      └─ WorkpaperRecordTemplateLightWithTemplatesDto
      └─ WorkpaperRecordTemplateUltraLightDto

