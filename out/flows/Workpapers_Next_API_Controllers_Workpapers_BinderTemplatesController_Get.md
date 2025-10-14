[web] GET /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Get)  [L49–L73] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to BinderRecordTemplateDto [L59]
  └─ maps_to BinderTemplateDto [L55]
    └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
  └─ calls ExcludedBinderTemplateRepository.ReadQuery [L70]
  └─ calls BinderTemplateRepository.ReadQuery [L55]
  └─ queries BinderTemplate [L55]
    └─ reads_from BinderTemplates
  └─ queries ExcludedBinderTemplate [L70]
    └─ reads_from ExcludedBinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method ReadQuery [L55]
  └─ uses_service IControlledRepository<ExcludedBinderTemplate>
    └─ method ReadQuery [L70]
  └─ uses_service UserService
    └─ method IsSuperUser [L53]

