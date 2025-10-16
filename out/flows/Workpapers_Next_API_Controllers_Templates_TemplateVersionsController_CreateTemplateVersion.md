[web] POST /api/template-versions  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.CreateTemplateVersion)  [L95–L110] status=201 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L109]
  └─ uses_service IMapper
    └─ method Map [L109]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Add [L107]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L103]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

