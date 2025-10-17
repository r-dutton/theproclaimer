[web] PUT /api/template-versions/{id:Guid}/activate  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.SetActive)  [L112–L124] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L123]
  └─ uses_service UnitOfWork
    └─ method Table [L116]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ TemplateVersionDto

