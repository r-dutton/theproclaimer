[web] PUT /api/templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplatesController.UpdateTemplate)  [L94–L107] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateDto [L106]
  └─ uses_service IMapper
    └─ method Map [L106]
  └─ uses_service UnitOfWork
    └─ method Table [L98]

