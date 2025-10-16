[web] GET /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Search)  [L60–L64] status=200
  └─ sends_request FindWorkpaperRecordTemplatesQuery [L63]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetWorkpaperRecordTemplatesPagedQueryHandler.Handle [L62–L277]
      └─ maps_to SectionLightDto [L111]
      └─ uses_service UserService
        └─ method IsSuperUser [L97]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L194]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L130]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L111]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L97]
          └─ ... (no implementation details available)

