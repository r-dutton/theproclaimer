[web] GET /api/workpaper-record-template-notes/for-workpaper-record-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.GetNotes)  [L42–L51] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to WorkpaperRecordTemplateNoteDto [L45]
  └─ uses_service UnitOfWork
    └─ method Table [L45]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperRecordTemplateNoteDto

