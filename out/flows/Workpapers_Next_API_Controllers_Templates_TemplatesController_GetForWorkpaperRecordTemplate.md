[web] GET /api/templates/for-workpaper-record-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplatesController.GetForWorkpaperRecordTemplate)  [L50–L61] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateDto [L54]
  └─ uses_service UnitOfWork
    └─ method Table [L54]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ TemplateDto

