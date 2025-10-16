[web] DELETE /api/workpaper-record-template-notes/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplateNotesController.Delete)  [L84–L92] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L87]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

