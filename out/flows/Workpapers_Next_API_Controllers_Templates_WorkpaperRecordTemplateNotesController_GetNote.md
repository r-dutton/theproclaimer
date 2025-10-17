[web] GET /api/workpaper-record-template-notes/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.GetNote)  [L53–L60] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to WorkpaperRecordTemplateNoteDto [L56]
  └─ uses_service UnitOfWork
    └─ method Table [L56]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperRecordTemplateNoteDto

