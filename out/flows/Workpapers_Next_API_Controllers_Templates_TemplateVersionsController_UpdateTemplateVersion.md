[web] PUT /api/template-versions/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.UpdateTemplateVersion)  [L126–L141] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L140]
  └─ uses_service IMapper
    └─ method Map [L140]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L130]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L136]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

