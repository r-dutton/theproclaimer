[web] PUT /api/templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplatesController.UpdateTemplate)  [L94–L107] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateDto [L106]
  └─ uses_service UnitOfWork
    └─ method Table [L98]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ TemplateDto

