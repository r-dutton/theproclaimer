[web] GET /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Get)  [L49–L73] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to BinderRecordTemplateDto [L59]
  └─ maps_to BinderTemplateDto [L55]
    └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
  └─ calls ExcludedBinderTemplateRepository.ReadQuery [L70]
  └─ calls BinderTemplateRepository.ReadQuery [L55]
  └─ query BinderTemplate [L55]
    └─ reads_from BinderTemplates
  └─ query ExcludedBinderTemplate [L70]
    └─ reads_from ExcludedBinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method ReadQuery [L55]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<ExcludedBinderTemplate>
    └─ method ReadQuery [L70]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L53]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

