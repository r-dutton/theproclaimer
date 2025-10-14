[web] PUT /api/template-versions/{id:Guid}/activate  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.SetActive)  [L112–L124] [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L123]
  └─ uses_service IMapper
    └─ method Map [L123]
  └─ uses_service UnitOfWork
    └─ method Table [L116]

