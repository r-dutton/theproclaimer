[web] PUT /api/template-versions/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.UpdateTemplateVersion)  [L126–L141] [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L140]
  └─ uses_service IMapper
    └─ method Map [L140]
  └─ uses_service UnitOfWork
    └─ method Table [L130]
  └─ uses_service UserService
    └─ method GetUserId [L136]

