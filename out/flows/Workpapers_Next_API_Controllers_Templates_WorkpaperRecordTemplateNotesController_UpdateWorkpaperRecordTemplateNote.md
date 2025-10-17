[web] PUT /api/workpaper-record-template-notes/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.UpdateWorkpaperRecordTemplateNote)  [L73–L82] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to WorkpaperRecordTemplateNoteDto [L81]
  └─ uses_service UnitOfWork
    └─ method Table [L76]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperRecordTemplateNoteDto

