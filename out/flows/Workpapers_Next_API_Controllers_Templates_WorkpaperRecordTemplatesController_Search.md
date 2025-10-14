[web] GET /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Search)  [L60–L64]
  └─ sends_request FindWorkpaperRecordTemplatesQuery [L63]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetWorkpaperRecordTemplatesPagedQueryHandler.Handle [L62–L277]
      └─ maps_to SectionLightDto [L111]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L194]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L130]
      └─ uses_service IMapper
        └─ method Map [L111]
      └─ uses_service UnitOfWork
        └─ method Table [L97]
      └─ uses_service UserService
        └─ method IsSuperUser [L97]

