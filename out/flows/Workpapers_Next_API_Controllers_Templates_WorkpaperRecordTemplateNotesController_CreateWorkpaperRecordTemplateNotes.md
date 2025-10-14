[web] POST /api/workpaper-record-template-notes  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.CreateWorkpaperRecordTemplateNotes)  [L62–L71] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to WorkpaperRecordTemplateNoteDto [L70]
  └─ uses_service IMapper
    └─ method Map [L70]
  └─ uses_service UnitOfWork
    └─ method Add [L68]
  └─ uses_service UserService
    └─ method GetUserId [L65]

