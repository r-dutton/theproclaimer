[web] POST /api/template-versions  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.CreateTemplateVersion)  [L95–L110] [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L109]
  └─ uses_service IMapper
    └─ method Map [L109]
  └─ uses_service UnitOfWork
    └─ method Add [L107]
  └─ uses_service UserService
    └─ method GetUserId [L103]

