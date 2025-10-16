[web] POST /api/workpaper-record-template-notes  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.CreateWorkpaperRecordTemplateNotes)  [L62–L71] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to WorkpaperRecordTemplateNoteDto [L70]
  └─ uses_service UnitOfWork
    └─ method Add [L68]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method GetUserId [L65]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ WorkpaperRecordTemplateNoteDto

