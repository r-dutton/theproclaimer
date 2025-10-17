[web] GET /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Search)  [L60–L64] status=200
  └─ sends_request FindWorkpaperRecordTemplatesQuery -> GetWorkpaperRecordTemplatesPagedQueryHandler [L63]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetWorkpaperRecordTemplatesPagedQueryHandler.Handle [L62–L277]
      └─ maps_to SectionLightDto [L111]
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L194]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L130]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service UnitOfWork
        └─ method Table [L97]
          └─ implementation UnitOfWork.Table
      └─ uses_service UserService
        └─ method IsSuperUser [L97]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ requests 1
      └─ FindWorkpaperRecordTemplatesQuery
    └─ handlers 1
      └─ GetWorkpaperRecordTemplatesPagedQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ SectionLightDto

