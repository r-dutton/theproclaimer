[web] POST /api/workpaper-record-template-notes  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.CreateWorkpaperRecordTemplateNotes)  [L62–L71] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to WorkpaperRecordTemplateNoteDto [L70]
  └─ uses_service IMapper
    └─ method Map [L70]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Add [L68]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L65]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

