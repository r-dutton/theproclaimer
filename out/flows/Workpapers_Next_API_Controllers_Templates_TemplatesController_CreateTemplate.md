[web] POST /api/templates  (Workpapers.Next.API.Controllers.Templates.TemplatesController.CreateTemplate)  [L79–L92] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateDto [L91]
  └─ uses_service IMapper
    └─ method Map [L91]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Add [L89]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L87]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

