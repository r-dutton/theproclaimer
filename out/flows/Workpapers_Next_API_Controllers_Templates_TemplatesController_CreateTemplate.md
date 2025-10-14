[web] POST /api/templates  (Workpapers.Next.API.Controllers.Templates.TemplatesController.CreateTemplate)  [L79–L92] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateDto [L91]
  └─ uses_service IMapper
    └─ method Map [L91]
  └─ uses_service UnitOfWork
    └─ method Add [L89]
  └─ uses_service UserService
    └─ method GetUserId [L87]

